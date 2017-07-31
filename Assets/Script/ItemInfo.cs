using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    // 재료 고유 번호
    public int id { get; set; }

    // 재료 기준 표 index
    public int index { get; set; }

    // 재료 이름
    public string mtName { get; set; }

    // 재료 등급
    public int grade { get; set; }

    // 재료 속성
    public int element { get; set; }

    // 재료 얻는 방법
    public int getRoot { get; set; }

    // 재료 판매 가격
    public int sellPrice { get; set; }

    // 재료 설명
    public string description { get; set; }

    // 재료 이미지 경로
    public string imagePath { get; set; }

    // 없어진지 확인하는 플래그
    public bool checkDestroy { get; set; }

    public void Init(int _id, int _index, string _name, int _grade, int _element, int _getRoot, int _sellPrice, string _description, string _imagePath)
    {
        id = _id;
        index = _index;
        mtName = _name;
        grade = _grade;
        element = _element;
        getRoot = _getRoot;
        sellPrice = _sellPrice;
        description = _description;
        imagePath = _imagePath;
        checkDestroy = false;
    }
}