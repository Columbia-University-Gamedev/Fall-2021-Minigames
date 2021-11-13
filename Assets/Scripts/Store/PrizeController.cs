using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PrizeController : MonoBehaviour
{
    public List<Prizes> prizeList;
    public int selected_prize;
    public GameObject base_game_obj;
    public List<GameObject> prize_objects;

    public GameObject selector;
    public Vector3 selector_target;
    public Text object_name;
    public Text object_price;

    public Sprite sold_out;

    // Start is called before the first frame update
    void Start()
    {
        SetupPrizes();
    }

    void SetupPrizes(){
        Grid grid = this.GetComponent<Grid>();
       
        for(int i=0; i< prizeList.Count; i++){
            int colNum = i/3;
            int rowNum = i % 3;
            Vector3Int gridPosition = new Vector3Int(rowNum, -colNum, 0);
            Vector3 cellPositionWorld = grid.CellToWorld(gridPosition);

            GameObject new_obj = Instantiate(base_game_obj,
                cellPositionWorld,Quaternion.identity,this.transform);
            //new_obj.transform.localScale = new_obj.transform.localScale * object_scale;
            new_obj.GetComponent<SpriteRenderer>().sprite = prizeList[i].is_bought ? 
                sold_out : prizeList[i].prize_art;
            prize_objects.Add(new_obj);

        }

        selector = Instantiate(selector,prize_objects[0].transform.position,Quaternion.identity);
        LoadSelection();

    }
    void OnSelect()
    {
        if (prizeList[selected_prize].is_bought) return;
        if (!MoneyController.SpendTickets(prizeList[selected_prize].price)) return;
        prizeList[selected_prize].is_bought = true;
        prize_objects[selected_prize].GetComponent<SpriteRenderer>().sprite = sold_out;
        Debug.Log(prizeList[selected_prize].is_bought);

        LoadSelection();
       
    }

    void LoadSelection() {
        selector_target = prize_objects[selected_prize].transform.position;
        
        object_name.text = prizeList[selected_prize].prize_name;
        object_price.text = prizeList[selected_prize].is_bought ? 
            "SOLD OUT" : prizeList[selected_prize].price.ToString();
    }

    void OnYMovement(InputValue res) 
    {
        float val = res.Get<float>();
        int indexChange = (val > 0) ? -3 : 3;
        selected_prize = Mathf.Clamp(selected_prize+indexChange,0, prizeList.Count-1);
        LoadSelection();
    }
     void OnXMovement(InputValue res) 
    {
        float val = res.Get<float>();
        int indexChange = (val > 0) ? 1 : -1;
        selected_prize = Mathf.Clamp(selected_prize+indexChange,0, prizeList.Count-1);
        LoadSelection();
    }

    public float lerpFactor;
    // Update is called once per frame
    void Update()
    {
        selector.transform.position = Vector3.Lerp(selector.transform.position,selector_target,
            Time.deltaTime*lerpFactor);
    }
}
