using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorDinosaurPool : ServicePool<RaptorDinosaurController>
{
    RaptorDinosaurModel RaptorDinosaurModel;
    RaptorDinosaurView RaptorDinosaurView; 

    public RaptorDinosaurController GetItem(RaptorsType raptorsType)
    {
        if(pooledItems.Count > 0)
       {
           PoolItem<RaptorDinosaurController> item = pooledItems.Find( I => I.isUsed == false);

           if(item != null)
           { 
              RaptorDinosaurModel model = (RaptorDinosaurModel)item.Item.WalkableDinosaurModel;
              if( model.RaptorsType == raptorsType) 
               { 
                   item.isUsed = true;
                   return  item.Item; 
               }
           }
       }
       // create new item for pool
       return createNewPooledItem();
    }


    public RaptorDinosaurController GetRaptorDinosaur(RaptorDinosaurModel Model ,RaptorDinosaurView View)
    {
          RaptorDinosaurModel = Model;
          RaptorDinosaurView = View;
          return GetItem(RaptorDinosaurModel.RaptorsType);
    }

    protected override RaptorDinosaurController CreateItem()
	{ 
 		RaptorDinosaurController raptorDinosaurController = new RaptorDinosaurController(RaptorDinosaurModel,RaptorDinosaurView);
        return raptorDinosaurController;
    }
}


