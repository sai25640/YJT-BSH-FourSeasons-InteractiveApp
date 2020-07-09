using Common;
using UnityEngine;
using QFramework;
using EventType = Common.EventType;
using System;
using System.Collections.Generic;

namespace FourSeasons
{
    public partial class EffectBuilder : MonoBehaviour
    {

        void Start()
        {
            // Code Here
            EventCenter.AddListener(EventType.SpringBegin, OnSpringBegin);
            EventCenter.AddListener(EventType.SpringEnd, OnSpringEnd);
            EventCenter.AddListener(EventType.SummerBegin, OnSummerBegin);
            EventCenter.AddListener(EventType.SummerEnd, OnSummerEnd);
            EventCenter.AddListener(EventType.FallBegin, OnFallBegin);
            EventCenter.AddListener(EventType.FallEnd, OnFallEnd);
            EventCenter.AddListener(EventType.WinterBegin, OnWinterBegin);
            EventCenter.AddListener(EventType.WinterEnd, OnWinterEnd);
        }

        void OnDestroy()
        {
            EventCenter.RemoveListener(EventType.SpringBegin, OnSpringBegin);
            EventCenter.RemoveListener(EventType.SpringEnd, OnSpringEnd);
            EventCenter.RemoveListener(EventType.SummerBegin, OnSummerBegin);
            EventCenter.RemoveListener(EventType.SummerEnd, OnSummerEnd);
            EventCenter.RemoveListener(EventType.FallBegin, OnFallBegin);
            EventCenter.RemoveListener(EventType.FallEnd, OnFallEnd);
            EventCenter.RemoveListener(EventType.WinterBegin, OnWinterBegin);
            EventCenter.RemoveListener(EventType.WinterEnd, OnWinterEnd);
        }

        #region 春
        private void OnSpringBegin()
        {
            for (int i = 0; i < 2; i++)
            {
                BuildSpringEffect("SpringEffect");
                BuildSpringEffect("SpringEffect2");
                BuildSpringEffect("SpringEffect3");
                BuildSpringEffect("SpringEffect4");
            }
        }

        private void BuildSpringEffect(string name)
        {
            var effect = ResLoader.Allocate().LoadSync<GameObject>(name).Instantiate()
                .GetComponent<SpringEffect>();
            effect.Init(transform, name);
        }

        private void OnSpringEnd()
        {
            transform.DestroyAllChild();
        }


        #endregion

        #region 夏
        private void OnSummerBegin()
        {
            for (int i = 0; i < 2; i++)
            {
                BuildSummerEffect("SummerEffect");
                BuildSummerEffect("SummerEffect2");
                BuildSummerEffect("SummerEffect3");
                BuildSummerEffect("SummerEffect4");
            }
        }

        private void BuildSummerEffect(string name)
        {
            var effect = ResLoader.Allocate().LoadSync<GameObject>(name).Instantiate()
                .GetComponent<SummerEffect>();
            effect.Init(transform, name);
        }

        private void OnSummerEnd()
        {
            transform.DestroyAllChild();
        }


        #endregion

        #region 秋
        private void OnFallBegin()
        {
            //for (int i = 0; i < 2; i++)
            //{
              
            //}
            BuildFallEffect("FallEffect");
            BuildFallEffect("FallEffect2");
            BuildFallEffect("FallEffect3");
            BuildFallEffect("FallEffect4");
        }

        private void BuildFallEffect(string name)
        {
            var effect = ResLoader.Allocate().LoadSync<GameObject>(name).Instantiate()
                .GetComponent<FallEffect>();
            effect.Init(transform, name);
        }

        private void OnFallEnd()
        {
            transform.DestroyAllChild();
        }


        #endregion

        #region 冬
        private void OnWinterBegin()
        {
            BuildWinterEffect("WinterEffect");
            BuildWinterEffect("WinterEffect2");
        }

        private void BuildWinterEffect(string name)
        {
            var effect = ResLoader.Allocate().LoadSync<GameObject>(name).Instantiate()
                .GetComponent<WinterEffect>();
            effect.Init(transform, name);
        }

        private void OnWinterEnd()
        {
            transform.DestroyAllChild();
        }



        #endregion


    }
}
