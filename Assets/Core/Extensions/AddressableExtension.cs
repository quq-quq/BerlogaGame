#if UnityEditor
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Core.Extensions
{
    public static class AddressableExtension
    {
        public static void MakeAddressable(this Object obj, string key = "", string groupName = "Default Local Group", IEnumerable<string> labels = null)
        {
            var path = AssetDatabase.GetAssetPath(obj);
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            var group = settings.GetOrCreateGroup(groupName);
            var guid = AssetDatabase.AssetPathToGUID(path);
            var entry = settings.CreateOrMoveEntry(guid, group);
            
            if(!string.IsNullOrEmpty(key))
                entry.address = key;
            
            if(labels != null)
            {
                foreach(var label in labels)
                {
                    if (!settings.GetLabels().Contains(label))
                    {
                        settings.AddLabel(label);
                        settings.SetDirty(AddressableAssetSettings.ModificationEvent.LabelAdded, label, true);
                    }
                    entry.labels.Add(label);
                }
            }
            
            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
            AssetDatabase.SaveAssets();
        }

        public static AddressableAssetGroup GetOrCreateGroup(this AddressableAssetSettings settings, string groupName)
        {
            var res = settings.FindGroup(groupName);
            if (res != null) return res;
            
            res = settings.CreateGroup(groupName, false, false, false, settings.DefaultGroup.Schemas);
            settings.SetDirty(AddressableAssetSettings.ModificationEvent.GroupAdded, res, true);
            AssetDatabase.SaveAssets();
            return res;
        }
    }
}
#endif