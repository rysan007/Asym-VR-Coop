using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    //private void Start()
    //{
    //    //print(Reverse("baka"));
    //    //print(ReverseRecursive("Racecar"));

        

    //    int[] numbers = new int[9] { 1,3,2,3,2,3,4,3,1 };
    //    print("odd num is " + FindOddNumber(numbers));
    //}

    //public int FindOddNumber(int[] input)
    //{
    //    Dictionary<int, int> dict = new Dictionary<int, int>();
    //    int oddNumber = 0;
    //    foreach (int num in input)
    //    {
    //        if (dict.ContainsKey(num))
    //        {
    //            dict[num]++;
    //        }
    //        else
    //        {
    //            dict.Add(num,1);
    //        }
    //    }

    //    foreach(var dic in dict)
    //    {
    //        if(dic.Value%2 != 0)
    //        {
    //            oddNumber = dic.Key;
    //        }
    //    }

    //    return oddNumber;
    //}


    //public string Reverse(string input)
    //{
    //    string reversedString = "";
    //    for(int i = input.Length - 1; i >= 0; i--)
    //    {
    //        reversedString += input[i];
    //    }
    //    return reversedString;
    //}

    //public string ReverseRecursive(string input)
    //{
    //    if(input == "")
    //    {
    //        return "";
    //    }
    //    if(input.Length == 1)
    //    {
    //        return input;
    //    }

    //    return input[input.Length - 1] + ReverseRecursive(input.Substring(0,input.Length-1));
    //}

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        //if (Input.GetKey(KeyCode.L))
        //{
        //    if (NetworkManager.Instance.Networker.IsServer)
        //        SceneManager.LoadScene(2, LoadSceneMode.Single);
        //}
    }
}
