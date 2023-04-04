using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Purchasing.Runtime.Unity;
using UnityEditor;

namespace UGF.Module.Purchasing.Editor.Unity
{
    [CustomEditor(typeof(PurchaseUnityModuleAsset), true)]
    internal class PurchaseUnityModuleAssetEditor : UnityEditor.Editor
    {
        private AssetIdReferenceListDrawer m_listProducts;
        private ReorderableListSelectionDrawer m_listProductsSelection;

        private void OnEnable()
        {
            m_listProducts = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_products"))
            {
                DisplayAsSingleLine = true
            };

            m_listProductsSelection = new ReorderableListSelectionDrawerByPath(m_listProducts, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listProducts.Enable();
            m_listProductsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listProducts.Disable();
            m_listProductsSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listProducts.DrawGUILayout();
                m_listProductsSelection.DrawGUILayout();
            }
        }
    }
}
