using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    private GameManager manager;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        GoTransparent();
        if (health <= 0)
        {
            Die();
        }
    }

    private void GoTransparent()
    {
        SkinnedMeshRenderer[] skinnedMeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        //Debug.Log(skinnedMeshRenderers.Length);
        //Debug.Log(meshRenderers.Length);
        foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
        {
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, health*0.02f);
            //Debug.Log("transparencied");
        }
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, health*0.02f);
            //Debug.Log("transparencied");
        }
    }

    void Die()
    {
        manager.playersObjAlive.Remove(gameObject);

        Destroy(gameObject.GetComponent<Rigidbody>());
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.GetComponent<GunJoystick>().enabled = false;
        gameObject.GetComponentInChildren<CameraController>().enabled = false;
        
        foreach (Transform child in transform)
        {
            if (!(child.gameObject.name == "Target" || child.gameObject.name == "Camera"))
                child.gameObject.SetActive(false);
        }

        enabled = false;
    }
}
