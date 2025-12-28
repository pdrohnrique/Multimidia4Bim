using UnityEngine;
using Unity.Cinemachine;

public class CameraAreaSwitcher : MonoBehaviour
{
    public CinemachineCamera vcam;
    public CinemachineConfiner2D confiner;

    public Collider2D recepcaoBounds;
    public Collider2D corredorBounds;
    public Collider2D laboratorioBounds;

    public void SetArea(string area)
    {
        switch (area)
        {
            case "Recepcao":
                confiner.BoundingShape2D = recepcaoBounds;
                break;
            case "Corredor":
                confiner.BoundingShape2D = corredorBounds;
                break;
            case "Laboratorio":
                confiner.BoundingShape2D = laboratorioBounds;
                break;
        }

        // Forçar o confiner a recalcular os limites
        confiner.InvalidateBoundingShapeCache();
    }
}