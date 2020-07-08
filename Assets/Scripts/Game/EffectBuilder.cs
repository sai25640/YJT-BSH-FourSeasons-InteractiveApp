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

        private void OnSpringBegin()
        {
            for (int i = 0; i < 8; i++)
            {
                var effect = ResLoader.Allocate().LoadSync<GameObject>("SpringEffect").Instantiate()
                                    .GetComponent<SpringEffect>();
                effect.Init(transform);
            }
        }

        private void OnSpringEnd()
        {
            transform.DestroyAllChild();
        }

        private void OnSummerBegin()
        {
            for (int i = 0; i < 8; i++)
            {
                var effect = ResLoader.Allocate().LoadSync<GameObject>("SummerEffect").Instantiate()
                    .GetComponent<SummerEffect>();
                effect.Init(transform);
            }
        }

        private void OnSummerEnd()
        {
            transform.DestroyAllChild();
        }

        private void OnFallBegin()
        {

        }

        private void OnFallEnd()
        {

        }

        private void OnWinterBegin()
        {

        }

        private void OnWinterEnd()
        {

        }


    }
}
