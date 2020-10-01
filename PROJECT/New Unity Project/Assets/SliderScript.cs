using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    private stategen state;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void setState(GameObject new_state)
    {
        state = new_state.GetComponent<stategen>(); 
    }

    // Update is called once per frame
    public void ChangeFT() {Debug.Log(slider.value); }
    public void ChangeInv() { Debug.Log(slider.value); }
    public void ChangeRes() { Debug.Log(slider.value); }
    public void ChangeCiv() {  Debug.Log(slider.value); }
    public void ChangeProd() {  Debug.Log(slider.value); }
}
