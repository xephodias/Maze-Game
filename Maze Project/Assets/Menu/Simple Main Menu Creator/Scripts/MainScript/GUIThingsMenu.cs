using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
public class GUIThingsMenu : MonoBehaviour {
	
	//GUISkin
	public GUISkin guiSkin = null;

	//Color 
	public Color ColorText;
	public bool UseColorText;

	//Transperency
	public float Transparency;
	public bool UseTransparancy;

	//Buttons position
	public int[] xPos = new int[30];
	public int[] yPos = new int[30];
	public int[] width = new int[30];
	public int[] height = new int[30];

	public int[] PlusX = new int[30];
	public int[] PlusY = new int[30];

	public Rect[] RectsOfButtons = new Rect[30];

	//Custom settings (buttons)
	public bool UseDefaultSettings;
	public bool UseCustomSettings;
	public bool UseCustomPosition;
	public bool[] UseCustomPlusPositions = new bool[30];
	public string[] labelsOfButtons = new string[30];
	public int CurButtons;
	public Texture2D[] OnHoverBtn = new Texture2D[30];
	public int[] SceneNumbers = new int[30];
	public bool[] IsPlaySound = new bool[30];
	public AudioClip[] PlaySound = new AudioClip[30];
	public int[] startbtnpage = new int[30];
	public int[] changebtnpage = new int[30];
	public int[] qualitynum = new int[30];
	private int CurPage = 0;

	//Textures
	public Rect[] RectsOfTextures = new Rect[30];
	public int TextureCount=1;
	public Texture2D[] Textures = new Texture2D[30];
	public bool UseCustomPositionTexture;
	public int[] XpositionTexture = new int[30];
	public int[] YpositionTexture = new int[30];
	public int[] WidthTexture = new int[30];
	public int[] HeightTexture = new int[30];
	public bool[] UseBoxBehind = new bool[30];

   	//Lists
    public enum Option {
        Center = 0,
        UpperRightCorner = 1,
        UpperLeftCorner = 2,
        BottomRightCorner = 3,
        BottomLeftCorner = 4
    }
    public Option opt;

    public enum BoxOption {
        Center = 0,
		AboveTheAllComponents=1
    }
    public BoxOption boxopt;

    public enum ListOfDo {
        ChangeLevel = 0,
        ExitApplication = 1,
		ChangePage = 2,
		ChangeQuality = 3
    }
	public ListOfDo[] btnDo = new ListOfDo[30];


	public enum TexturePlacement {
		NextToTheButtons = 0
	}
	public TexturePlacement textureplacement;

	public enum TextureNearButtonPlacement {
		Left = 0,
		Right = 1,
		Up = 2,
		Down = 3,
		Inside = 4
	}
	public int[] NearButtonTextureNum = new int[30];
	public TextureNearButtonPlacement[] nearbtnplacement = new TextureNearButtonPlacement[20];

	//Preview Texture settings
	public enum PreviewTexturePlacement {
		LeftSide = 0,
		RightSide = 1,
		MiddleOfTheTop = 2,
		MidBottom = 3,
		UpperRightCorner=4,
		UpperLeftCorner=5,
		BottomRightCorner=6,
		BottomLeftCorner=7,
		AboveTheHighestButton=8
	}
	public PreviewTexturePlacement previewplacement;
	public Texture2D previewtexture;
	public bool UsePreviewTexture_Interface = false;
	public bool UsePreviewTexture = false;
	public int widthPreviewTexture = 0;
	public int heightPreviewTexture = 0;

    //Boxes settings
    public int CurBoxes;
	public Rect[] RectsBoxes = new Rect[30];
	public int[] XposBoxes = new int[30];
	public int[] YposBoxes = new int[30];
	public int[] WidthBoxes = new int[30];
	public int[] HeightBoxes = new int[30];
	public string[] textOfBoxes = new string[30];
	public bool UseCustomPosBox;
	private int minX, minY, maxY, maxX, AllWidth, AllHeight;
	public string[] boxName = new string[30];

	//Animation settings
	public float _transparency = 0f;
	public bool UseStartAnimation = false;
	public float speedAnimation = 0f;
	public bool StartAnim_Interface = false;

	void Update() {
		for (int i = 0; i < 10; ++i) {
			if (!UseCustomPlusPositions [i]) {
				PlusX [i] = 0;
				PlusY [i] = 0;
			}
		}
	}

	void FixedUpdate() {
		if (UseStartAnimation) {
			if (_transparency < Transparency)
				_transparency += speedAnimation;
		}
	}

    void OnGUI() {
		if(guiSkin != null)
			GUI.skin = guiSkin;
		
		if (UseCustomPosBox) {
			for (int i = 0; i < CurBoxes; ++i) {
				RectsBoxes [i] = new Rect (XposBoxes [i], YposBoxes [i], WidthBoxes [i], HeightBoxes [i]);
			}
		} else {
			switch (boxopt) {
			case (BoxOption.AboveTheAllComponents):
				maxY = -10000; minY = 10000; maxX = -10000; minX = 10000; AllWidth = -10000;
				for (int L = 0; L < CurButtons; L++) {
					if (CurPage == startbtnpage [L]) {
						if (maxY < RectsOfButtons [L].y + RectsOfButtons [L].height)
							maxY = (int)(RectsOfButtons [L].y + RectsOfButtons [L].height);
						if (minY > RectsOfButtons [L].y)
							minY = (int)RectsOfButtons [L].y;
						if (maxX < RectsOfButtons [L].x + RectsOfButtons [L].width)
							maxX = (int)(RectsOfButtons [L].x + RectsOfButtons [L].width);
						if (minX > RectsOfButtons [L].x)
							minX = (int)RectsOfButtons [L].x;
						if (AllWidth < (int)RectsOfButtons [L].width)
							AllWidth = (int)RectsOfButtons [L].width;
					}

				}
				break;

			case (BoxOption.Center):
				for (int i = 1; i < CurBoxes + 1; ++i) {
					int xBox = Screen.width / 2 - WidthBoxes [i] / 2;
					int yBox = 0;

					if (i > 0)
						yBox = (int)RectsBoxes [i - 1].y + (int)RectsBoxes [i - 1].height;
					else
						yBox = Screen.height / 2 - height [i] / 2;
				
					RectsBoxes [0] = new Rect (Screen.width / 2 - WidthBoxes [0] / 2, Screen.height / 2 - HeightBoxes [0] / 2, WidthBoxes [0], HeightBoxes [0]);
					RectsBoxes [i] = new Rect (xBox, yBox, WidthBoxes [i], HeightBoxes [i]);
				}

				break;
			}
		}
	
		if(UseCustomPosition) {
			for (int i = 0; i < CurButtons; ++i) {
				RectsOfButtons[i] = new Rect(xPos[i], yPos[i], width[i], height[i]);
			}
		} else {
			switch(opt) {
			case(Option.Center):
				for(int i = 1; i < CurButtons+1; ++i) {
					int Xbtn = Screen.width/2 - width[i]/2;
					int yBtn = 0; 

					if(i > 0) yBtn = (int)RectsOfButtons[i-1].y + (int)RectsOfButtons[i-1].height;
					else yBtn = Screen.height/4 - height[i]/4;

					RectsOfButtons[0] = new Rect(Screen.width/2 - width[0]/2+PlusX[0], Screen.height/4 - height[0]/4+PlusY[0], width[0], height[0]);
					RectsOfButtons[i] = new Rect(Xbtn + PlusX[i], yBtn + PlusY[i], width[i], height[i]);
				}
				break;

			case(Option.UpperLeftCorner) :
				for(int i = 0; i < CurButtons; ++i) {
					if(i == 0)RectsOfButtons[i] = new Rect(PlusX[i], PlusY[i]+30, width[i], height[i]);
					else RectsOfButtons[i] = new Rect(PlusX[i],(int)RectsOfButtons[i-1].y+(int)RectsOfButtons[i-1].height+PlusY[i],width[i], height[i]);
				}
				break;

			case(Option.UpperRightCorner) :
				for (int i = 0; i < CurButtons; ++i) {
					if(i==0) RectsOfButtons[i] = new Rect(Screen.width-width[i]+PlusX[i], PlusY[i]+30, width[i], height[i]);
					else RectsOfButtons[i] = new Rect(Screen.width-width[i]+PlusX[i], 
						(int)RectsOfButtons[i-1].y+(int)RectsOfButtons[i-1].height+PlusY[i], width[i], height[i]);
					}

				break;

			case (Option.BottomLeftCorner) :
				for (int i = 0; i < CurButtons; ++i){
					if (i == 0) RectsOfButtons[i] = new Rect(PlusX[i], Screen.height - height[i] + PlusY[i], width[i], height[i]);
					else RectsOfButtons[i] = new Rect(PlusX[i], RectsOfButtons[i - 1].y - height[i]+PlusY[i], width[i], height[i]);
            	}
				break;

			case(Option.BottomRightCorner) :
				for (int i = 0; i < CurButtons; ++i) {
					if(i==0) RectsOfButtons[i] = new Rect(Screen.width-width[i]+PlusX[i], Screen.height - height[i]+PlusY[i], width[i], height[i]);
					else RectsOfButtons[i] = new Rect(Screen.width-width[i]+PlusX[i], RectsOfButtons[i - 1].y - height[i] + PlusY[i], width[i], height[i]);
				}

				break;
			}
		}

		if (UseStartAnimation)
			GUI.color = new Color (ColorText.r, ColorText.g, ColorText.b, _transparency / 1000);
		else
			GUI.color = new Color (ColorText.r, ColorText.g, ColorText.b, Transparency);

		if (!UseCustomPosBox && boxopt == BoxOption.AboveTheAllComponents) {
			GUI.Box (new Rect (minX - 5, minY - 30, maxX - minX + 10, maxY - minY + 40), boxName [CurPage]);
		}
		else {
			for (int i = 0; i < CurBoxes; ++i) {
				GUI.Box (RectsBoxes[i], textOfBoxes[i]);
			}
		}

        for (int i = 0; i < CurButtons; ++i) {
			if (startbtnpage [i] == CurPage) {
				if (GUI.Button (RectsOfButtons [i], labelsOfButtons [i])) {
					switch (btnDo [i]) {
					case ListOfDo.ChangeLevel:
						SceneManager.LoadScene (SceneNumbers [i]);
						break;
					case ListOfDo.ExitApplication:
						Application.Quit ();
						break;
					case ListOfDo.ChangePage:
						CurPage = changebtnpage [i];
						break;
					case ListOfDo.ChangeQuality:
						QualitySettings.SetQualityLevel (qualitynum[i]);
						break;
					}

					if (IsPlaySound [i] && PlaySound[i] != null) {
						AudioSource.PlayClipAtPoint (PlaySound[i], transform.position);
					}
				}
			}
        }

		GUI.color = Color.white;

		for (int i = 0; i < TextureCount; ++i) {
			if (UseCustomPositionTexture) {
				if (UseBoxBehind[i])
					GUI.Box (new Rect (XpositionTexture [i], YpositionTexture [i], WidthTexture [i], HeightTexture [i]), "");
				
				GUI.DrawTexture (new Rect (XpositionTexture [i], YpositionTexture [i], WidthTexture [i], HeightTexture [i]), Textures [i]);
			} else {
				if(Textures[i] != null) {
				switch (nearbtnplacement[i]) {
					case TextureNearButtonPlacement.Inside:
						if(UseBoxBehind[i])
							GUI.Box (new Rect (RectsOfButtons [NearButtonTextureNum [i] - 1].x, RectsOfButtons [NearButtonTextureNum [i] - 1].y, 
								RectsOfButtons [NearButtonTextureNum [i] - 1].width, RectsOfButtons [NearButtonTextureNum [i] - 1].height), "");
						
						GUI.DrawTexture (new Rect (RectsOfButtons [NearButtonTextureNum [i] - 1].x, RectsOfButtons [NearButtonTextureNum [i] - 1].y, 
							RectsOfButtons [NearButtonTextureNum [i] - 1].width, RectsOfButtons [NearButtonTextureNum [i] - 1].height), Textures [i]);
						break;

					case TextureNearButtonPlacement.Right:
						if(UseBoxBehind[i])
							GUI.Box (new Rect (RectsOfButtons[NearButtonTextureNum[i]-1].x+RectsOfButtons[NearButtonTextureNum[i]-1].width, 
								RectsOfButtons[NearButtonTextureNum[i]-1].y, WidthTexture[i], HeightTexture[i]), "");

						GUI.DrawTexture (new Rect (RectsOfButtons[NearButtonTextureNum[i]-1].x+RectsOfButtons[NearButtonTextureNum[i]-1].width, 
							RectsOfButtons[NearButtonTextureNum[i]-1].y, WidthTexture[i], HeightTexture[i]) , Textures[i]);
						break;
					case TextureNearButtonPlacement.Left:
						if(UseBoxBehind[i])
							GUI.Box (new Rect (RectsOfButtons[NearButtonTextureNum[i]-1].x-WidthTexture[i], 
								RectsOfButtons[NearButtonTextureNum[i]-1].y, WidthTexture[i], HeightTexture[i]) , "");

						GUI.DrawTexture (new Rect (RectsOfButtons[NearButtonTextureNum[i]-1].x-WidthTexture[i], 
							RectsOfButtons[NearButtonTextureNum[i]-1].y, WidthTexture[i], HeightTexture[i]) , Textures[i]);
						break;
					case TextureNearButtonPlacement.Up:
						if(UseBoxBehind[i])
							GUI.Box (new Rect (RectsOfButtons[NearButtonTextureNum[i]-1].x, 
								RectsOfButtons[NearButtonTextureNum[i]-1].y-HeightTexture[i], WidthTexture[i], HeightTexture[i]) , "");
						
						GUI.DrawTexture (new Rect (RectsOfButtons[NearButtonTextureNum[i]-1].x, 
							RectsOfButtons[NearButtonTextureNum[i]-1].y-HeightTexture[i], WidthTexture[i], HeightTexture[i]) , Textures[i]);
						break;
					case TextureNearButtonPlacement.Down:
						if(UseBoxBehind[i])
							GUI.Box (new Rect (RectsOfButtons[NearButtonTextureNum[i]-1].x, 
								RectsOfButtons[NearButtonTextureNum[i]-1].y+RectsOfButtons[NearButtonTextureNum[i]-1].height, WidthTexture[i], HeightTexture[i]) , "");
						
						GUI.DrawTexture (new Rect (RectsOfButtons[NearButtonTextureNum[i]-1].x, 
							RectsOfButtons[NearButtonTextureNum[i]-1].y+RectsOfButtons[NearButtonTextureNum[i]-1].height, WidthTexture[i], HeightTexture[i]) , Textures[i]);
						break;
					}
				}
			}
		}
		if (UsePreviewTexture) {
			switch (previewplacement) {
			case PreviewTexturePlacement.MiddleOfTheTop:
				GUI.DrawTexture (new Rect (Screen.width / 2 - widthPreviewTexture / 2, 0, widthPreviewTexture, heightPreviewTexture), previewtexture);
				break;
			case PreviewTexturePlacement.MidBottom:
				GUI.DrawTexture (new Rect (Screen.width / 2 - widthPreviewTexture / 2, Screen.height - heightPreviewTexture, widthPreviewTexture, heightPreviewTexture), previewtexture);
				break;
			case PreviewTexturePlacement.LeftSide:
				GUI.DrawTexture (new Rect (0, Screen.height / 2 - heightPreviewTexture / 2, widthPreviewTexture, heightPreviewTexture), previewtexture);
				break;
			case PreviewTexturePlacement.RightSide:
				GUI.DrawTexture (new Rect (Screen.width - widthPreviewTexture, Screen.height / 2 - heightPreviewTexture / 2, widthPreviewTexture, heightPreviewTexture), previewtexture);
				break;
			case PreviewTexturePlacement.UpperLeftCorner:
				GUI.DrawTexture (new Rect (0, 0, widthPreviewTexture, heightPreviewTexture), previewtexture);
				break;
			case PreviewTexturePlacement.UpperRightCorner:
				GUI.DrawTexture (new Rect (Screen.width - widthPreviewTexture, 0, widthPreviewTexture, heightPreviewTexture), previewtexture);
				break;
			case PreviewTexturePlacement.BottomLeftCorner:
				GUI.DrawTexture (new Rect (0, Screen.height - heightPreviewTexture, widthPreviewTexture, heightPreviewTexture), previewtexture);
				break;
			case PreviewTexturePlacement.BottomRightCorner:
				GUI.DrawTexture (new Rect (Screen.width - widthPreviewTexture, Screen.height - heightPreviewTexture, widthPreviewTexture, heightPreviewTexture), previewtexture);
				break;
			case PreviewTexturePlacement.AboveTheHighestButton:
				float minY_p = 100000;
				float minX_p = 100000;
				float maxX_p = -100000;
				for (int i = 0; i < CurButtons; ++i) {
					if (minY_p > RectsOfButtons [i].y)
						minY_p = RectsOfButtons [i].y;
					if (minX_p > RectsOfButtons [i].x)
						minX_p = RectsOfButtons [i].x;
					if (maxX_p < RectsOfButtons [i].x)
						maxX_p = RectsOfButtons [i].x+RectsOfButtons[i].width;
				}

				GUI.DrawTexture (new Rect ((int)(maxX_p + minX_p) / 2 - widthPreviewTexture/2, (int)minY_p - heightPreviewTexture-30, widthPreviewTexture, heightPreviewTexture), previewtexture); 
				break;
			}
		}
	}
}
