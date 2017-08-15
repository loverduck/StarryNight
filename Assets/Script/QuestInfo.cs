using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo : MonoBehaviour {

    // 퀘스트 기준 표 index
    public int index { get; set; }

    // 퀘스트 발생 액트
    public string act { get; set; }

    public string title { get; set; }

    public string content { get; set; }

    public int termsItem { get; set; }

    public int termsCount { get; set; }

    public int reward { get; set; }

    public int rewardCount { get; set; }

    public void Init(int _index, string _act, string _title, string _content, int _termsItem, int _termsCount, int _reward, int _rewardCount)
    {
        index = _index;
        act = _act;
        title = _title;
        content = _content;
        termsItem = _termsItem;
        termsCount = _termsCount;
        reward = _reward;
        rewardCount = _rewardCount;
    }
}
