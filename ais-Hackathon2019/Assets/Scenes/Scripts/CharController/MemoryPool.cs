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
    // メモリープール生成
    // original : コーピーする見本
    // count : 最大サイズ
    //-------------------------------------------------------------------------------------
    public void Create(Object original, int count)
    {
        Dispose();    // メモリープール初期化
        table = new Item[count]; // 配列生成

        for (int i = 0; i < count; i++)
        {
            Item item = new Item();
            item.active = false;
            item.gameObject = GameObject.Instantiate(original) as GameObject;
            // originalをGameObject形式でitem.gameObjectに
            item.gameObject.SetActive(false);
            // 非活性にする
            table[i] = item;
        }
    }

    //-------------------------------------------------------------------------------------
    // 新しいアイテム要求 - 非活性状態のアイテムを活性状態にする
    //-------------------------------------------------------------------------------------
    public GameObject NewItem()
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
    // アイテム要求取消 - 活性状態のアイテムを非活性にする
    // gameOBject : NewItemメソッドで活性状態になった変数
    //--------------------------------------------------------------------------------------
    public void RemoveItem(GameObject gameObject)
    {
        // tableが生成されてないか, gameObjectがない場合
        if (table == null || gameObject == null)
            return;

        // tableかつgameObjectが存在すればここから
        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            // gameObjectとitemのgameObject同じ場合
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