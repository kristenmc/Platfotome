using UnityEditor;
using UnityEngine;

namespace Platfotome.CustomEditors {

	class CustomPostprocessors : AssetPostprocessor {

		void OnPreprocessTexture() {
			if (assetPath.Contains("Assets/_Platfotome/Textures")) {
				TextureImporter importer = (TextureImporter)assetImporter;
				importer.spritePixelsPerUnit = 16;
				importer.textureCompression = TextureImporterCompression.Uncompressed;
				importer.filterMode = FilterMode.Point;
			}
		}

		//void OnPreprocessModel() {
		//	if (assetPath.Contains("Meshes")) {
		//		var importer = (ModelImporter)assetImporter;
		//		importer.useFileScale = false;
		//		importer.importVisibility = false;
		//		importer.importCameras = false;
		//		importer.importLights = false;

		//		importer.importAnimation = false;

		//		importer.importMaterials = false;
		//	}
		//}

	}

}