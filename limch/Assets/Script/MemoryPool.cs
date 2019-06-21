using UnityEngine;
using System.Collections;

//-----------------------------------------------------------------------------------------
// メモリープールクラス
// GameObjectを生成、削除せず、予め生成して置いたGameObjectをリサイクルする機能
//-----------------------------------------------------------------------------------------

public class MemoryPool : IEnumerable, System.IDisposable
{
    class Item
    {
        public bool active; // GameObject使用中かどうかを判定
        public GameObject gameObject;
    }

    // GameObjectを保存しておく配列
    Item[] table;

    public IEnumerator GetEnumerator()
    {
        if (table == null)
            yield break;    

        // tableがある場合  
        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if (item.active) // itemが使用中の場合
                yield return item.gameObject;
        }
    }

    //-------------------------------------------------------------------------------------
    // 메모리 풀 생성
    // original : 미리 생성해 둘 원본소스
    // count : 풀 최고 갯수
    //-------------------------------------------------------------------------------------
    public void Create(Object original, int count)
    {
        Dispose();    // 메모리풀 초기화
        table = new Item[count]; // count 만큼 배열을 생성

        for (int i = 0; i < count; i++) // count 만큼 반복
        {
            Item item = new Item();
            item.active = false;
            item.gameObject = GameObject.Instantiate(original) as GameObject;
            // original을 GameObject 형식으로 item.gameObject에 저장
            item.gameObject.SetActive(false);
            // SetActive는 활성화 함수인데 메모리에만 올릴 것이므로 비활성화 상태로 저장
            table[i] = item;
        }
    }

    //-------------------------------------------------------------------------------------
    // 새 아이템 요청 - 쉬고 있는 객체를 반납한다.
    //-------------------------------------------------------------------------------------
    public GameObject NewItem() // GetEnumerator()와 비슷
    {
        if (table == null)
            return null;
        int count = table.Length;
        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if (item.active == false)
            {
                item.active = true;
                item.gameObject.SetActive(true);
                return item.gameObject;
            }
        }

        return null;
    }

    //--------------------------------------------------------------------------------------
    // 아이템 사용종료 - 사용하던 객체를 쉬게한다.
    // gameOBject : NewItem으로 얻었던 객체
    //--------------------------------------------------------------------------------------
    public void RemoveItem(GameObject gameObject)
    {
        // table이 객체화되지 않았거나, 매개변수로 오는 gameObject가 없다면
        if (table == null || gameObject == null)
            return; // 함수 탈출

        // table이 존재하거나, 매개변수로 오는 gameObject가 존재하면 여기서부터 실행
        // count는 table의 길이(즉, 배열의 크기)
        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            // 매개변수 gameObject와 item의 gameObject가 같다면
            if (item.gameObject == gameObject)
            {
                item.active = false;
                item.gameObject.SetActive(false);
                break;
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // すべてのGameObjectを非活性する
    //--------------------------------------------------------------------------------------
    public void ClearItem()
    {
        // GameObjectが生成されない場合
        if (table == null)
            return;

        // GameObjectが生成された場合
        // 生成されたGameObjectの数
        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            // itemがNOT NULL、アクティブの場合
            if (item != null && item.active)
            {
                item.active = false;
                item.gameObject.SetActive(false);
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // メモリープール削除
    //--------------------------------------------------------------------------------------
    public void Dispose()
    {
        // GameObjectが生成されない場合
        if (table == null)
            return;

        // GameObjectが生成された場合
        // 生成されたGameObjectの数
        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            GameObject.Destroy(item.gameObject);
            // すべてのGameObjectをDestroy
        }
        table = null;
    }

}