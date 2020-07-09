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
