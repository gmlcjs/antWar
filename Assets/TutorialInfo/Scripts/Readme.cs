using System;
using UnityEngine;

public class Readme : ScriptableObject
{
    public Texture2D icon; // 아이콘
    public string title; // 제목
    public Section[] sections; // 섹션
    public bool loadedLayout; // 레이아웃 로드 여부

    [Serializable]
    public class Section 
    { 
    // 섹션의 제목, 내용, 링크 텍스트, 링크 URL
        public string heading, text, linkText, url;
    }
}
