using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexDinosaurPool : ServicePool<TRexDinosaurController>
{
    TRexView trexView ;
    TRexDinosaurModel trexDinosaurModel;

    public TRexDinosaurController GetItem(TRexType trexType)
    {
        if(pooledItems.Count > 0)
       {
           PoolItem<TRexDinosaurController> item = pooledItems.Find( I => I.isUsed == false);
           
           
           if(item != null)
           {
              TRexDinosaurModel model = (TRexDinosaurModel)item.Item.WalkableDinosaurModel;
              if(model.TRexType == trexType) 
              { 
                 item.isUsed = true;
                 return  item.Item; 
               }
            } 
       }
       // create new item for pool
       return createNewPooledItem();
    }

    public TRexDinosaurController GetTRexDinosaur(TRexDinosaurModel trexDinosaurModel, TRexView trexView)
    {
        this.trexDinosaurModel = trexDinosaurModel;
        this.trexView = trexView;
        return this.GetItem(trexDinosaurModel.TRexType);
    }

    protected override TRexDinosaurController CreateItem()
	{
		TRexDinosaurController trexDinosaurController = new TRexDinosaurController(trexDinosaurModel,trexView);
        return trexDinosaurController;
	}

}