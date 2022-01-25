using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    private float constructionTimer;
    private float constructionTimerMax;
    private SO_BuildingType buildingType;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = transform.GetComponent<BuildingTypeHolder>();
        constructionMaterial = spriteRenderer.material;
    }
    public static BuildingConstruction Create(Vector3 position,SO_BuildingType buildingType)
    {
        Transform pf_BuildingConstruction = Resources.Load<Transform>("PF_BuildingConstruction");
        Transform buildingConstructionTransform= Instantiate(pf_BuildingConstruction, position, Quaternion.identity);
        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetupBuildingType(buildingType);
        return buildingConstruction;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        constructionTimer -= Time.deltaTime;
        constructionMaterial.SetFloat("_Progress",GetConstructionTimerNormalized());
        if (constructionTimer <= 0f)
        {
            constructionTimer += constructionTimerMax;
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
    private void SetupBuildingType(SO_BuildingType buildingType)
    {
        this.constructionTimerMax = buildingType.constructionTimerMax;
        constructionTimer = this.constructionTimerMax;
        spriteRenderer.sprite = buildingType.buildingSprite;

        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
        this.buildingType = buildingType;
        buildingTypeHolder.buildingType = buildingType;
    }

    public float GetConstructionTimerNormalized()
    {
        return  (1 - constructionTimer / constructionTimerMax);
    }
}
