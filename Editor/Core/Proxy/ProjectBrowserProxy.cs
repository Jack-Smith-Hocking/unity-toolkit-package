using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LordSheo.Editor
{
	public static class ProjectBrowserProxy
	{
		public static Type type;

		public static FieldInfo lastInteractedProjectBrowserField;
		public static FieldInfo lastFoldersField;

		public static MethodInfo getFolderInstanceIdMethod;
		public static MethodInfo setFolderSelectionMethod;

		static ProjectBrowserProxy()
		{
			type = Type.GetType("UnityEditor.ProjectBrowser, UnityEditor.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

			lastInteractedProjectBrowserField = type.FindFieldInfo_Static("s_LastInteractedProjectBrowser");
			lastFoldersField = type.FindFieldInfo_Instance("m_LastFolders");

			getFolderInstanceIdMethod = type.FindMethodInfo_Static("GetFolderInstanceID", new Type[] { typeof(string) });
#if UNITY_6000_4_OR_NEWER
			setFolderSelectionMethod = type.FindMethodInfo_Instance("SetFolderSelection", new Type[] { typeof(EntityId[]), typeof(bool) });
#else
			setFolderSelectionMethod = type.FindMethodInfo_Instance("SetFolderSelection", new Type[] { typeof(int[]), typeof(bool) });
#endif
		}

		public static EditorWindow GetLastBrowser()
		{
			return (EditorWindow)lastInteractedProjectBrowserField.GetValue(null);
		}
		public static string[] GetLastFolders(EditorWindow browser)
		{
			return (string[])lastFoldersField.GetValue(browser);
		}

		public static int GetFolderInstanceID(string folder)
		{
#if UNITY_6000_4_OR_NEWER
			var result = getFolderInstanceIdMethod.Invoke(null, new object[] { folder });
			return (int)EntityId.ToULong((EntityId)result);
#else
			return (int)getFolderInstanceIdMethod.Invoke(null, new object[] { folder });
#endif
		}
		public static void SetFolderSelection(EditorWindow browser, int[] folderIds)
		{
#if UNITY_6000_4_OR_NEWER
			var folderEntityIds = folderIds
				.Select(i => EntityId.FromULong((ulong)i))
				.ToArray();
			
			setFolderSelectionMethod.Invoke(browser, new object[] { folderEntityIds, true });
#else
			setFolderSelectionMethod.Invoke(browser, new object[] { folderIds, true });
#endif
		}
	}
}
