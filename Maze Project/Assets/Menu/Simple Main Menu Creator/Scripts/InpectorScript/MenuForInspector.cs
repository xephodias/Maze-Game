#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEditor;
[System.Serializable]
[CustomEditor(typeof(GUIThingsMenu))]
[CanEditMultipleObjects]

public class MenuForInspector : Editor {
    //Variables
    private bool UseColorText = false;
    private bool UseTransperency = false;
	private Vector2 scrollposition;
	private Vector2 scrollpositionBox;
	private Vector2 scrollpositionTextures;

	void GUIStyleSettings(GUIStyle style, int fontSize, FontStyle fontsl, TextAnchor textanch) {
        style.richText = true;
		style.alignment = textanch;
		style.fontStyle = fontsl;
		style.fontSize = fontSize;
    }

	public override void OnInspectorGUI() {
		BigSpace (1, false, 0);

        GUIThingsMenu script = (GUIThingsMenu)target;
		GUIStyle style = new GUIStyle();
		GUIStyle mainstyle = new GUIStyle ();
		GUIStyleSettings(style, 20, FontStyle.Italic, TextAnchor.MiddleCenter);
		GUIStyleSettings (mainstyle, 20, FontStyle.Bold, TextAnchor.LowerCenter);

		EditorGUILayout.LabelField (@"Main Menu Creator", mainstyle);

		script.guiSkin = (GUISkin)EditorGUILayout.ObjectField("GUI skin: ", script.guiSkin, typeof(GUISkin), true);

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Components", style);

		BigSpace(3,false,0);
        
        //Color settings
		if(GUILayout.Button("Сolor of buttons")) {
			UseColorText=true;
			script.UseColorText=true;
		}

		if(UseColorText){
			EditorGUILayout.BeginVertical("box"); {
				EditorGUILayout.LabelField ("Color", style);
				BigSpace (2, false, 0);
				script.ColorText = EditorGUI.ColorField (GUILayoutUtility.GetRect (15, 15), script.ColorText);

				if (GUILayout.Button ("Close Menu")) {
					UseColorText = false;
				}
				EditorGUILayout.EndVertical ();
			}
		}

		BigSpace(1,false,0);

		//Transparency settings
		if(GUILayout.Button("Transparency")) {
			UseTransperency=true;
			script.UseTransparancy = true;
		}
		if(UseTransperency==true) {
			EditorGUILayout.BeginVertical("box"); {
				EditorGUILayout.LabelField ("Transparency", style);
				EditorGUILayout.Space ();

				script.Transparency = EditorGUILayout.IntSlider ((int)script.Transparency, 0, 1000, null);
				ProgressBar ((float)script.Transparency / 1000, "Transparency");

				if (GUILayout.Button ("Close Menu")) {
					UseTransperency = false;
				}
				EditorGUILayout.EndVertical ();
			}
		}

		BigSpace(1,false,0);

		if (GUILayout.Button ("Animations")) {
			script.StartAnim_Interface = true;
		}

		if (script.StartAnim_Interface) {
			EditorGUILayout.BeginVertical ("box"); {
				EditorGUILayout.LabelField ("Start Animation:", style);
				script.UseStartAnimation = GUILayout.Toggle (script.UseStartAnimation, "Use animation:");
				if (script.UseStartAnimation) {
					script.speedAnimation = EditorGUILayout.FloatField ("Speed:", script.speedAnimation);
					EditorGUILayout.HelpBox (@" 1 - Very Slow. 
				10 - Normal.
				30 - Very fast.", MessageType.Info);
				}
				if (GUILayout.Button ("Close Menu")) {
					script.StartAnim_Interface = false;
				}
				EditorGUILayout.EndVertical();
			}
		}

		BigSpace (1, false, 0);

		if (GUILayout.Button ("Preview Texture")) {
			script.UsePreviewTexture_Interface = true;
		}

		if (script.UsePreviewTexture_Interface) {
			EditorGUILayout.BeginVertical ("box"); {
				EditorGUILayout.LabelField ("Preview Texture:", style);
				script.UsePreviewTexture = GUILayout.Toggle (script.UsePreviewTexture, "Use Preview Texture:");
				if (script.UsePreviewTexture) {
					script.previewtexture = (Texture2D)EditorGUI.ObjectField(GUILayoutUtility.GetRect(15,15,""), "Texture:", script.previewtexture, typeof(Texture2D), true);
					script.previewplacement = (GUIThingsMenu.PreviewTexturePlacement)EditorGUILayout.EnumPopup ("Placement:", script.previewplacement);
					script.widthPreviewTexture = EditorGUILayout.IntField ("Width:", script.widthPreviewTexture);
					script.heightPreviewTexture = EditorGUILayout.IntField ("Height:", script.heightPreviewTexture);
				}

				if (GUILayout.Button ("Close")) {
					script.UsePreviewTexture_Interface = false;
				}

				EditorGUILayout.EndVertical ();
			}
		}

		BigSpace(5,false,3);
		
        //Button system
		EditorGUILayout.LabelField("Buttons", style);
		BigSpace(2,false,0);

        if (script.CurButtons < 15) {
            if (GUILayout.Button("Add button")) {
                script.CurButtons++;
            }
        }

		script.UseCustomPosition = GUILayout.Toggle(script.UseCustomPosition, "Use Custom Position");

        if (!script.UseCustomPosition) {
            script.opt = (GUIThingsMenu.Option)EditorGUILayout.EnumPopup("Placement:", script.opt);
        }

		BigSpace(2, true, 1);
    
		if(script.CurButtons>=3)
			scrollposition =
				EditorGUILayout.BeginScrollView (scrollposition, false, true, GUILayout.Height (500));
		
		for (int i = 0; i < script.CurButtons; ++i) {
            EditorGUILayout.BeginVertical("box"); {
                EditorGUILayout.LabelField("Button " + Convert.ToString(i + 1), EditorStyles.boldLabel);
                if (script.UseCustomPosition) {
                    script.xPos[i] = EditorGUILayout.IntField("X position:", script.xPos[i]);
                    script.yPos[i] = EditorGUILayout.IntField("Y position:", script.yPos[i]);
                }
                script.width[i] = EditorGUILayout.IntField("Width:", script.width[i]);
                script.height[i] = EditorGUILayout.IntField("Height:", script.height[i]);

                EditorGUILayout.Space();

                script.labelsOfButtons[i] = EditorGUILayout.TextField("Label of Button:", script.labelsOfButtons[i]);
                EditorGUILayout.Space();

                script.UseCustomPlusPositions[i] = GUILayout.Toggle(script.UseCustomPlusPositions[i], "Add coordinates?");
                if (script.UseCustomPlusPositions[i]){
                    script.PlusX[i] = EditorGUILayout.IntField("Add X:", script.PlusX[i]);
                    script.PlusY[i] = EditorGUILayout.IntField("Add Y:", script.PlusY[i]);
                }

                script.btnDo[i] = (GUIThingsMenu.ListOfDo)EditorGUILayout.EnumPopup("Function:", script.btnDo[i]);
                if (script.btnDo[i] == GUIThingsMenu.ListOfDo.ChangeLevel) {
                    script.SceneNumbers[i] = EditorGUILayout.IntField("Level Number:", script.SceneNumbers[i]);
                }

				if (script.btnDo [i] == GUIThingsMenu.ListOfDo.ChangePage) {
					script.changebtnpage[i] = EditorGUILayout.IntField ("Change page to:", script.changebtnpage[i]);
				}

				if (script.btnDo [i] == GUIThingsMenu.ListOfDo.ChangeQuality) {
					script.qualitynum [i] = EditorGUILayout.IntField ("Quality Level:", script.qualitynum[i]);
				}

                EditorGUILayout.Space();

                script.IsPlaySound[i] = GUILayout.Toggle(script.IsPlaySound[i], "Play sound?");
                if(script.IsPlaySound[i]) {
					script.PlaySound[i] = (AudioClip)EditorGUILayout.ObjectField("Sound: ", script.PlaySound[i], typeof(AudioClip), true);
                }

				script.startbtnpage[i] = EditorGUILayout.IntField ("Page:", script.startbtnpage[i]);
                EditorGUILayout.EndVertical();
            }
		}

		if(script.CurButtons>=3)
			EditorGUILayout.EndScrollView();   	 

        if (script.CurButtons > 0) {
			if(GUILayout.Button("Remove Button")) {
			    script.CurButtons--;
		    }
		}

		BigSpace(5,false,3);

		//Box system

		EditorGUILayout.LabelField("Boxes", style);
		BigSpace(1,false,0);

		script.UseCustomPosBox = GUILayout.Toggle(script.UseCustomPosBox, "Use Custom Position");
		if(!script.UseCustomPosBox)  
			script.boxopt = (GUIThingsMenu.BoxOption)EditorGUILayout.EnumPopup("Placement: ", script.boxopt);

		if(script.CurBoxes < 10 && (script.boxopt == GUIThingsMenu.BoxOption.Center || script.UseCustomPosBox)) {
			if(GUILayout.Button("Add Box")) {
				script.CurBoxes++;
			}
		}

		if (script.UseCustomPosBox)
			script.boxopt = GUIThingsMenu.BoxOption.Center; 

		if (script.boxopt != GUIThingsMenu.BoxOption.AboveTheAllComponents) {
			
			if(script.CurBoxes >= 4)
				scrollpositionBox =
					EditorGUILayout.BeginScrollView (scrollpositionBox, false, true, GUILayout.Height (300));

			for(int i = 0; i < script.CurBoxes; ++i) {
				EditorGUILayout.BeginVertical("box"); {
					EditorGUILayout.LabelField("Box " + Convert.ToString(i + 1), EditorStyles.boldLabel);

					if (script.UseCustomPosBox) {
						script.XposBoxes[i] = EditorGUILayout.IntField("X position:", script.XposBoxes[i]);
						script.YposBoxes[i] = EditorGUILayout.IntField("Y position:", script.YposBoxes[i]);
					}

					script.WidthBoxes[i] = EditorGUILayout.IntField("Width:", script.WidthBoxes[i]);
					script.HeightBoxes[i] = EditorGUILayout.IntField("Height:", script.HeightBoxes[i]);

					EditorGUILayout.Space();

					script.textOfBoxes[i] = EditorGUILayout.TextField("Label of Box:", script.textOfBoxes[i]);
					EditorGUILayout.EndVertical();
				}
			}

			if(script.CurBoxes >= 4)
				EditorGUILayout.EndScrollView ();
		}

		switch(script.boxopt) {
		case GUIThingsMenu.BoxOption.AboveTheAllComponents:
			int pages = -1000;
			for (int i = 0; i < script.CurButtons; ++i) {
				if (pages < script.startbtnpage[i])
					pages = script.startbtnpage[i];
			}

			if (!script.UseCustomPosBox) {
				for (int i = 0; i < pages + 1; ++i) {
					script.boxName [i] = EditorGUILayout.TextField ("Page " + Convert.ToString (i + 1) + " title:", script.boxName [i]);
				}
				//script.textOfBoxes [0] = EditorGUILayout.TextField ("Label of Box:", script.textOfBoxes [0]);
			}


				break;
		}
		if (script.CurBoxes > 0 && (script.boxopt == GUIThingsMenu.BoxOption.Center || script.UseCustomPosBox)) {
			if (script.CurBoxes >= 1) {
				if (GUILayout.Button ("Remove Box")) {
					script.CurBoxes--;
				}
			}
		}

		BigSpace(5,false,3);

		//TextureSystem
		EditorGUILayout.LabelField("Textures", style);
		script.UseCustomPositionTexture = GUILayout.Toggle (script.UseCustomPositionTexture, "Use Custom Position:");
		if (script.TextureCount < 10) {
			if (GUILayout.Button ("Add Texture")) {
				script.TextureCount++;
			}
		}


		if(script.TextureCount>=3)
			scrollpositionTextures =
				EditorGUILayout.BeginScrollView (scrollpositionTextures, false, true, GUILayout.Height (450));
		
		for (int i = 0; i < script.TextureCount; ++i) {
			EditorGUILayout.BeginVertical("box"); {
				EditorGUILayout.LabelField("Texture " + Convert.ToString(i + 1), EditorStyles.boldLabel);

				script.Textures [i] = (Texture2D)EditorGUI.ObjectField (GUILayoutUtility.GetRect (15, 15), "Texture: ", script.Textures[i], typeof(Texture2D), true);
				if (script.UseCustomPositionTexture) {
					script.XpositionTexture [i] = EditorGUILayout.IntField ("X position:", script.XpositionTexture [i]);
					script.YpositionTexture [i] = EditorGUILayout.IntField ("Y position:", script.YpositionTexture [i]);
				} else {
					script.NearButtonTextureNum[i] = EditorGUILayout.IntField ("Button Number:", script.NearButtonTextureNum[i]);
					if (script.NearButtonTextureNum[i] < 1 || script.NearButtonTextureNum[i] > script.CurButtons)
						script.NearButtonTextureNum[i] = 1;

					script.nearbtnplacement [i] = (GUIThingsMenu.TextureNearButtonPlacement)EditorGUILayout.EnumPopup ("Placement: ", script.nearbtnplacement [i]);
				}

				script.WidthTexture [i] = EditorGUILayout.IntField ("Width:", script.WidthTexture [i]);
				script.HeightTexture [i] = EditorGUILayout.IntField ("Height:", script.HeightTexture [i]);

				script.UseBoxBehind [i] = GUILayout.Toggle (script.UseBoxBehind [i], "Use Box behind");

				EditorGUILayout.EndVertical();
			}
		}

		if (script.TextureCount >= 3)
			EditorGUILayout.EndScrollView ();

		if (script.TextureCount != 0) {
			if (GUILayout.Button ("Remove Texture")) {
				script.TextureCount--;
			}
		}

		BigSpace (3, false, 0);
		GUILayout.Label ("Powered by FixThis studio");
    }

	void ProgressBar (float value, string text) {
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value, text);
		EditorGUILayout.Space ();
	}

	void BigSpace (int value, bool UseLine, int size) {
		for(int i = 0; i < value; ++i) {
			EditorGUILayout.Space();
			if (i == value/2) {
				if(UseLine)
					EditorGUI.DrawRect(GUILayoutUtility.GetRect(200,size), Color.black);
			}
		}
	}
}
#endif 