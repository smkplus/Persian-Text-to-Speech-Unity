using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech : MonoBehaviour {
    public string Text;
    public List<string> characters;
    public AudioSource audioSource;

    void Start(){
        Text = Text.Replace(" ","");
        MergeSameSound();
        SeperateCharacters();

        StartCoroutine(Play());
    }


    //یکی کردن صدا های یکسان
    private void MergeSameSound(){
    Text = Text.Replace("ض","ز");  
    Text = Text.Replace("ذ","ز");
    Text = Text.Replace("ظ","ز"); 

    Text = Text.Replace("ح","ه"); 

    Text = Text.Replace("غ","ق"); 

    Text = Text.Replace("ط","ت"); 

    Text = Text.Replace("ث","س"); 
    Text = Text.Replace("ص","س");   
    }

    //جدا کردن کاراکتر ها
    private void SeperateCharacters()
    {
        for (int i = 0; i < Text.Length; i++)
        {
            if(IsHarakat(i+1) || IsMaddi(i+1)){
            characters.Add(Text[i].ToString() + Text[i+1].ToString());
            }else if((!IsHarakat(i+1) && !IsHarakat(i)) && (!IsMaddi(i+1) && !IsMaddi(i))){
            characters.Add(Text[i].ToString());
            }
        }
    }

    bool IsMaddi(int i){
       return i < Text.Length && IsLike(Text[i],"وای"); 
    }
    

    bool IsHarakat(int i){
    return i < Text.Length && IsLike(Text[i]," َ ِ ُ");
    }
    bool CheckCharacter(int i,char character){
        
        return Text[i] == character;
    }

    //بررسی مجموعه کاراکتر
    private bool IsLike(char character,string collection){
        bool result = false;
        collection = collection.Replace(" ","");//حذف فاصله ها
        foreach (var ch in collection)
        {
            if(character == ch){
            result = true;
            }
        }
        return result;
    }

    int index;
    [Range(0.0f,0.5f)]
    public float cut;
    //پخش صدا
    IEnumerator Play(){
        var alphabetSound = (AudioClip)Resources.Load("Sounds/" + characters[index]);
        float duration = alphabetSound.length;
        audioSource.clip = alphabetSound;
        audioSource.Play();
        yield return new WaitForSeconds(duration-cut);
        if(index < characters.Count-1){
            index++;
            StartCoroutine(Play());
        }
    }
}
