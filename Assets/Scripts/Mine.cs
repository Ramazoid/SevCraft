using UnityEngine;
using Random = UnityEngine.Random;

public class Mine : MonoBehaviour,Informable
{
    public int ResourceCapacity;
    private int WorkersCapacity=500;
    private int WWorkersOnBoard=0;

    public string GetInfo()
    {

        return $"<B>{name}</b> \n Resources:\n{ResourceCapacity}\n Workers:\n{WWorkersOnBoard}/{WorkersCapacity}";
    }

    void Start()
    {
        ResourceCapacity = Random.Range(5, Settings.MineResourceMaxCapacity);
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
          Mining(col);
      }

    private void Mining(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Worker"))
        {

            Commanded worker = col.gameObject.GetComponent<Commanded>();
            if (WWorkersOnBoard < WorkersCapacity && ResourceCapacity > 0)
            {
                WWorkersOnBoard++;
                worker.Command("Mining", name, transform.position);
            }
            else
                worker.Command("Away!", "", Random.insideUnitSphere * 20);
        }
    }
    private void OnTriggerStay(Collider col)
    {
        Mining(col);
    }

    private void OnCollisionEnter(Collision col)
    {
        
    }



}
