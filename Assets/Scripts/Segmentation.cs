using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Segmentation : MonoBehaviour
{
    public GameObject segmentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Texture2D texture = (Texture2D) GetComponent<SpriteRenderer>().sprite.texture;
        int width = texture.width;
        int height = texture.height;

        float scale = 1f / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit; 

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Color color = texture.GetPixel(i, j);
                GameObject segment = Instantiate(segmentPrefab,
                new Vector3(i * scale, j * scale)
                - new Vector3((float)width * scale / 2f, (float)height * scale / 2f)
                + new Vector3(scale / 2f, scale / 2f),
                Quaternion.identity);
                segment.transform.localScale = new Vector3(scale, scale, scale);
                segment.transform.SetParent(transform, false);
                segment.GetComponent<SpriteRenderer>().material.color = color;
            }
        }
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
