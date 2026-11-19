using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour{
    
    PlayerMovement player;

    void Awake(){
        player = GetComponentInParent<PlayerMovement>();

    }

    public void ShootArrow(){ player.ShootArrow(); }
}
