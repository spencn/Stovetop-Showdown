using UnityEngine;

public class OffStage : MonoBehaviour
{
    public AllScores scores;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Pea")
            return;
        var cps = other.gameObject.GetComponent<PlayerControllerScript>();
        if (cps == null || cps.invulnerable)
            return;
        cps.kill();
        
        scores.AwardKnockOut(other.gameObject.GetComponent<Hitmarker>());
    }

}