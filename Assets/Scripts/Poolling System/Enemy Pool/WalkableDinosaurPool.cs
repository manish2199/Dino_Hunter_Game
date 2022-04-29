using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableDinosaurPool : ServicePool<WalkableDinosaurController>
{
    WalkableDinosaurModel WalkableDinosaurModel;
    WalkableDinosaurView WalkableDinosaurView; 

    protected override void Awake()
   {
       base.Awake();
    }

    public WalkableDinosaurController GetItem(WalkingDinosaurType walkingDinosaurType)
    {
        if(pooledItems.Count > 0)
       {
           PoolItem<WalkableDinosaurController> item = pooledItems.Find( I => I.isUsed == false);

           if(item != null)
           { 
            //   if( WalkableDinosaurModel.RaptorsType == raptorsType) 
              if(item.Item.WalkableDinosaurModel.WalkingDinosaurType == walkingDinosaurType) 
               {  
                   if(item.Item.WalkableDinosaurModel.WalkingDinosaurType == WalkingDinosaurType.Raptors)
                   {
                     RaptorDinosaurModel raptorModelInsidePool = (RaptorDinosaurModel)item.Item.WalkableDinosaurModel;
                     RaptorDinosaurModel raptoModelToCheck = (RaptorDinosaurModel)WalkableDinosaurModel;
                        
                       if(raptorModelInsidePool.RaptorsType == raptoModelToCheck.RaptorsType  )  
                       {
                          item.isUsed = true;
                          return  item.Item; 
                       }
                   }
                   if(item.Item.WalkableDinosaurModel.WalkingDinosaurType == WalkingDinosaurType.TRex)
                   {
                        TRexDinosaurModel trexModelInsidePool = (TRexDinosaurModel)item.Item.WalkableDinosaurModel;
                        TRexDinosaurModel trexModelToCheck = (TRexDinosaurModel)WalkableDinosaurModel;
                        
                        if(trexModelInsidePool.TRexType == trexModelToCheck.TRexType)
                        {
                          item.isUsed = true;
                          return  item.Item; 
                        }
                   }
               }
           }
       }
       // create new item for pool
       return createNewPooledItem();
    }


    public WalkableDinosaurController GetWalkableDinosaur(WalkableDinosaurModel Model ,WalkableDinosaurView View)
    {
          WalkableDinosaurModel = Model;
          WalkableDinosaurView = View;
          return GetItem(WalkableDinosaurModel.WalkingDinosaurType);
    }

    protected override WalkableDinosaurController CreateItem()
	{ 
 		WalkableDinosaurController walkableDinosaurController = null;
        //check which controller to create 
       if(WalkableDinosaurModel.WalkingDinosaurType == WalkingDinosaurType.Raptors)
       { 
            RaptorDinosaurModel raptorDinosaurModel = (RaptorDinosaurModel) WalkableDinosaurModel;
            RaptorDinosaurView raptorDinosaurView = (RaptorDinosaurView) WalkableDinosaurView;

            RaptorDinosaurController raptorDinosaurController = new RaptorDinosaurController(raptorDinosaurModel,raptorDinosaurView);
            walkableDinosaurController = raptorDinosaurController;
       }
       if(WalkableDinosaurModel.WalkingDinosaurType == WalkingDinosaurType.TRex)
       {
           TRexDinosaurModel trexModel = (TRexDinosaurModel) WalkableDinosaurModel;
           TRexView trexView = (TRexView) WalkableDinosaurView;
          
           TRexDinosaurController trexController = new TRexDinosaurController(trexModel,trexView);
            walkableDinosaurController = trexController;
       }

        return walkableDinosaurController;
    }
}
