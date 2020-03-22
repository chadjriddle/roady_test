using UnityEngine;
using WildbotLabs.Scriptables.References;

public class PlayerRCC : MonoBehaviour
{
    public BoolReference IsActive;

    public BoolReference PlayerInputLeft;
    public BoolReference PlayerInputRight;
    public BoolReference PlayerInputBackwardLeft;
    public BoolReference PlayerInputBackwardRight;

    private float gasInput = 0f;
    private float brakeInput = 0f;
    private float leftInput = 0f;
    private float rightInput = 0f;
    private float handbrakeInput = 0f;
    private float NOSInput = 1f;
    private bool canUseNos = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive.Value == false)
        {
            gasInput = 0.0f;
            brakeInput = 1.0f;
            leftInput = 0.0f;
            rightInput = 0.0f;
            FeedRCC();
            return;
        }

        if (PlayerInputBackwardLeft || PlayerInputBackwardRight)
        {
            gasInput =  0.0f;
            brakeInput = 1.0f;
        }
        else
        {
            gasInput = 1.0f;
            brakeInput = 0.0f;
        }

        leftInput = PlayerInputLeft || PlayerInputBackwardLeft ? 1.0f : 0.0f;
        rightInput = PlayerInputRight || PlayerInputBackwardRight ? 1.0f : 0.0f;
    
        FeedRCC();
    }

    private void FeedRCC(){

        if (!RCC_SceneManager.Instance.activePlayerVehicle)
            return;

        RCC_SceneManager.Instance.activePlayerVehicle.SetEngine(IsActive.Value);
        canUseNos = RCC_SceneManager.Instance.activePlayerVehicle.useNOS;

        if (RCC_SceneManager.Instance.activePlayerVehicle.canControl && !RCC_SceneManager.Instance.activePlayerVehicle.externalController) 
        {
            RCC_SceneManager.Instance.activePlayerVehicle.gasInput = gasInput;
            RCC_SceneManager.Instance.activePlayerVehicle.brakeInput = brakeInput;
            RCC_SceneManager.Instance.activePlayerVehicle.steerInput = -leftInput + rightInput;
            RCC_SceneManager.Instance.activePlayerVehicle.handbrakeInput = handbrakeInput;
            RCC_SceneManager.Instance.activePlayerVehicle.boostInput = NOSInput;
        }

    }
}
