using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public void Lvl1(){
		SceneManager.LoadScene ("basics");
		}
	public void Lvl2(){
		SceneManager.LoadScene ("Level2");
		}
	public void Lvl3(){
		SceneManager.LoadScene ("Level3");
	}


}
