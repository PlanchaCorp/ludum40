using System;
using System.Linq;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LibGit2Sharp;

namespace bTools.GitGud
{
	public class GitGudWindow : EditorWindow
	{
		// Settings
		[SerializeField] string username = string.Empty, commitName = string.Empty, email = string.Empty, password = string.Empty;

		// GUI
		private int selectedTab = 0;
		private string[] tabsText = new string[] { "Basic", "Advanced", "Settings" };
		private string commitMessage;

		// Dirty Thread Sharing.
		private Repository repo;
		private SyncStatus syncStatus = new SyncStatus();

		private Vector2 scrollPos;

		[MenuItem( "bTools/GitGud" )]
		static void Init()
		{
			var window = GetWindow<GitGudWindow>( string.Empty, true );

			window.titleContent = new GUIContent( "Git Gud" );
			window.minSize = new Vector2( 220, 100 );
		}

		private void OnEnable()
		{
			if ( repo == null ) repo = new Repository( Application.dataPath.Replace( "/Assets", string.Empty ) );
			//if ( repo == null ) repo = new Repository( @"D:\Blobinet\Downloads\OtherGitTest" );
		}

		private void OnDisable()
		{
			if ( repo != null ) repo.Dispose();
		}

		private void OnDestroy()
		{
			if ( repo != null ) repo.Dispose();
		}

		private void OnGUI()
		{
			GUILayout.BeginHorizontal( EditorStyles.toolbar );
			selectedTab = GUILayout.Toolbar( selectedTab, tabsText, EditorStyles.toolbarButton );
			GUILayout.EndHorizontal();

			switch ( selectedTab )
			{
				case 0:
					DrawBasicTab();
					break;
				case 1:
					DrawAdvancedTab();
					break;
				case 2:
					DrawSettingsTab();
					break;
				default:
					break;
			}

			if ( syncStatus.syncFinished )
			{
				syncStatus = new SyncStatus();
				commitMessage = string.Empty;
				EditorApplication.UnlockReloadAssemblies();
				AssetDatabase.Refresh();
			}
		}

		private void DrawBasicTab()
		{
			GUILayout.Space( 25 );

			var progressRect = EditorGUILayout.GetControlRect();
			var pullProgressRect = EditorGUILayout.GetControlRect();
			var pushProgressRect = EditorGUILayout.GetControlRect();

			lock ( syncStatus )
			{
				if ( syncStatus.progress > 0 ) EditorGUI.ProgressBar( progressRect, syncStatus.progress, syncStatus.progressText );
				if ( syncStatus.pullProgress > 0 ) EditorGUI.ProgressBar( pullProgressRect, syncStatus.pullProgress, syncStatus.pullProgressText );
				if ( syncStatus.pushProgress > 0 ) EditorGUI.ProgressBar( pushProgressRect, syncStatus.pushProgress, syncStatus.pushProgressText );
			}

			commitMessage = GUILayout.TextArea( commitMessage, GUILayout.Height( 45 ) );

			using ( new EditorGUI.DisabledGroupScope( syncStatus.syncRunning || !repo.Index.IsFullyMerged ) )
			{
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if ( GUILayout.Button( "Sync", GUILayout.Width( 160 ), GUILayout.Height( 45 ) ) )
				{
					syncStatus.syncFinished = false;
					syncStatus.syncRunning = true;
					EditorApplication.LockReloadAssemblies();
					ThreadPool.QueueUserWorkItem( new WaitCallback( DoSync ) );
				}
				GUILayout.FlexibleSpace();
				EditorGUILayout.EndHorizontal();
			}
			GUILayout.Space( 25 );

			using ( var scroll = new EditorGUILayout.ScrollViewScope( scrollPos ) )
			{
				scrollPos = scroll.scrollPosition;

				if ( repo != null && !repo.Index.IsFullyMerged )
				{
					EditorGUILayout.LabelField( "-- Merge Conflicts --", EditorStyles.boldLabel );
					#region ConflictSolving
					foreach ( var conflict in repo.Index.Conflicts )
					{
						//Debug.Log( conflict.Ours?.Path + '\n' + conflict.Theirs?.Path + '\n' + conflict.Ancestor?.Path );

						IndexEntry ancestor = conflict.Ancestor;
						IndexEntry ours = conflict.Ours;
						IndexEntry theirs = conflict.Theirs;

						if ( ancestor == null && ours == null )
						{
							Commands.Stage( repo, theirs.Path );
							Debug.LogWarning( "(ノಠ益ಠ)ノ彡┻━┻ GOT A WERID CONFLICT (ノಠ益ಠ)ノ彡┻━┻... Kept \"Theirs\" " + theirs.Path );
							continue;
						}
						if ( ancestor == null && theirs == null )
						{
							Commands.Remove( repo, ours.Path, true );
							Debug.LogWarning( "(ノಠ益ಠ)ノ彡┻━┻ GOT A WERID CONFLICT (ノಠ益ಠ)ノ彡┻━┻... Deleted \"Ours\" " + ours.Path );
							continue;
						}
						if ( ours == null && theirs == null )
						{
							Commands.Remove( repo, ancestor.Path, true );
							Debug.LogWarning( "(ノಠ益ಠ)ノ彡┻━┻ GOT A WEIRD CONFLICT (ノಠ益ಠ)ノ彡┻━┻... Deleted \"Ancestor\" " + ancestor.Path );
							continue;
						}

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField( ancestor.Path );

						if ( GUILayout.Button( "Ours" ) )
						{
							var blob = repo.Lookup<Blob>( conflict.Ours.Id );
							var stream = blob.GetContentStream();
							using ( var file = File.Open( repo.Info.WorkingDirectory + conflict.Theirs.Path, FileMode.Open ) )
							{
								file.SetLength( 0 );
								stream.CopyTo( file );
							}

							syncStatus.conflictsSolved = true;
							Commands.Stage( repo, conflict.Theirs.Path );
						}
						if ( GUILayout.Button( "Theirs" ) )
						{
							var blob = repo.Lookup<Blob>( conflict.Theirs.Id );
							var stream = blob.GetContentStream();
							using ( var file = File.Open( repo.Info.WorkingDirectory + conflict.Ours.Path, FileMode.Open ) )
							{
								file.SetLength( 0 );
								stream.CopyTo( file );
							}

							syncStatus.conflictsSolved = true;
							Commands.Stage( repo, conflict.Ours.Path );
						}

						EditorGUILayout.EndHorizontal();
					}
					#endregion
				}
			}
		}

		private void DrawAdvancedTab()
		{
			if ( GUILayout.Button( "Fix Compiling" ) )
			{
				EditorApplication.UnlockReloadAssemblies();
			}
		}

		private void DrawSettingsTab()
		{
			GUILayout.Label( "Username" );
			username = EditorGUILayout.TextField( username );
			GUILayout.Label( "Password" );
			password = EditorGUILayout.PasswordField( password );

			GUILayout.Label( "Name" );
			commitName = EditorGUILayout.TextField( commitName );
			GUILayout.Label( "Email" );
			email = EditorGUILayout.TextField( email );
		}

		private void DoSync( object a )
		{
			try
			{
				// Fetch
				lock ( syncStatus )
				{
					syncStatus.syncFinished = false;
					syncStatus.syncRunning = true;
					syncStatus.progress = 0.14f;
					syncStatus.progressText = "Starting Sync 1/7";
				}

				FetchOptions fo = new FetchOptions();
				Credentials credentials = new UsernamePasswordCredentials() { Username = username, Password = password };
				fo.CredentialsProvider = ( _url, _user, _cred ) => credentials;

				lock ( syncStatus )
				{
					syncStatus.progress = 0.28f;
					syncStatus.progressText = "Fetching 2/7";
				}

				Remote remote = repo.Network.Remotes["origin"];
				IEnumerable<string> refSpecs = remote.FetchRefSpecs.Select( x => x.Specification );
				string logMessage = "Fetch ";
				Commands.Fetch( repo, "origin", refSpecs, fo, logMessage );

				// Check if staging needed.
				TreeChanges localChanges = repo.Diff.Compare<TreeChanges>( repo.Head.Tip.Tree, DiffTargets.WorkingDirectory | DiffTargets.Index );

				// Stage All
				if ( localChanges.Count > 0 || syncStatus.conflictsSolved )
				{
					lock ( syncStatus )
					{
						syncStatus.conflictsSolved = false;
						syncStatus.progress = 0.42f;
						syncStatus.progressText = "Staging 3/7";
					}

					Commands.Stage( repo, "*" );
					Signature comitter = new Signature( commitName, email, DateTime.Now );
					if ( string.IsNullOrEmpty( commitMessage ) ) commitMessage = "No Message";
					repo.Commit( commitMessage, comitter, comitter );
				}

				lock ( syncStatus )
				{
					syncStatus.progress = 0.57f;
					syncStatus.progressText = "Pulling 4/7";
				}

				MergeOptions opts = new MergeOptions() { FileConflictStrategy = CheckoutFileConflictStrategy.Merge };
				opts.FindRenames = true;
				opts.OnCheckoutProgress += PullProgress;
				Signature merger = new Signature( commitName, email, DateTime.Now );
				repo.Merge( repo.Head.TrackedBranch, merger, opts );

				lock ( syncStatus )
				{
					syncStatus.progress = 0.71f;
					syncStatus.progressText = "Solving Conflicts 5/7";
				}

				// Resolve conflicts
				if ( repo.Index.Conflicts.Count() > 0 )
				{
					lock ( syncStatus )
					{
						syncStatus.syncFinished = true;
						syncStatus.syncRunning = false;
					}
					Debug.LogWarning( "Conflicts found ! Solve them and sync again !" );
					return;
				}

				var remoteChanges = repo.Commits.QueryBy( new CommitFilter
				{ IncludeReachableFrom = repo.Head.TrackedBranch.Tip.Id, ExcludeReachableFrom = repo.Head.Tip.Id } );

				if ( remoteChanges.Count() > 0 )
				{   // In case remote is ahead again after merge.
					syncStatus.syncFinished = true;
					syncStatus.syncRunning = false;
					Debug.LogWarning( "( ͡° ͜ʖ ͡°) Someone was faster, Sync again ! ( ͡° ͜ʖ ͡°)" );
					return;
				}

				lock ( syncStatus )
				{
					syncStatus.progress = 0.85f;
					syncStatus.progressText = "Pushing 6/7";
				}

				PushOptions po = new PushOptions();
				po.CredentialsProvider = ( _url, _user, _cred ) => credentials;
				po.OnPushTransferProgress += PushProgress;
				repo.Network.Push( repo.Branches["master"], po );

				lock ( syncStatus )
				{
					syncStatus.progress = 0.99f;
					syncStatus.progressText = "Finishing 7/7";
					syncStatus.syncFinished = true;
					syncStatus.syncRunning = false;
				}
			}
			catch ( Exception e )
			{
				Debug.LogError( e.Data );
				Debug.LogError( e.Message );
				Debug.LogError( e.InnerException );
				Debug.LogError( e.StackTrace );
				lock ( syncStatus )
				{
					syncStatus.syncFinished = true;
					syncStatus.syncRunning = false;
				}
				throw;
			}
		}

		private bool PushProgress( int current, int total, long bytes )
		{
			lock ( syncStatus )
			{
				if ( current == total )
				{
					syncStatus.pushProgress = 0.0f;
					syncStatus.pushProgressText = string.Empty;
				}
				else
				{
					syncStatus.pushProgress = (float)current / (float)total;
					syncStatus.pushProgressText = $"Pushing file {current} out of {total}";
				}
			}
			return true;
		}

		private void PullProgress( string path, int completedSteps, int totalSteps )
		{
			lock ( syncStatus )
			{
				if ( completedSteps == totalSteps )
				{
					syncStatus.pullProgress = 0.0f;
					syncStatus.pullProgressText = string.Empty;
				}
				else
				{
					syncStatus.pullProgress = (float)completedSteps / (float)totalSteps;
					syncStatus.pullProgressText = "Pulling: " + path;
				}
			}
		}
	}

	internal class SyncStatus
	{
		public bool syncFinished;
		public bool syncRunning;
		public bool conflictsSolved;

		public float progress;
		public string progressText;

		public float pushProgress;
		public string pushProgressText;

		public float pullProgress;
		public string pullProgressText;
	}
}
