using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using EntityStates;
using RoR2.Projectile;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Hex3Mod;
using Hex3Mod.Logging;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Behaviors
{
    // Item behavior to track characterbody skills/stacks/stats
    public class StarHomeRunnerBehavior : CharacterBody.ItemBehavior
    {
        private void Awake()
        {
            base.enabled = false;
        }
		private void OnEnable()
        {
            if (this.body)
            {
                this.body.onSkillActivatedServer += this.OnSkillActivated;
                this.skillLocator = this.body.GetComponent<SkillLocator>();
                this.inputBank = this.body.GetComponent<InputBankTest>();
			}
        }
		private void OnDisable()
		{
			if (this.body)
			{
				this.body.onSkillActivatedServer -= this.OnSkillActivated;
			}
			this.inputBank = null;
			this.skillLocator = null;
		}
		private void OnSkillActivated(GenericSkill skill)
		{
			SkillLocator skillLocator = this.skillLocator;
			if (skill != null && skill == skillLocator.primary && this.reloaded == true)
			{
				this.Swing();
			}
		}
		private void FixedUpdate()
		{
			if (this.reloaded == false)
            {
				reloadTimer += Time.deltaTime;
			}
			if (reloadTimer >= totalReloadTime)
            {
				this.reloaded = true;
				reloadTimer = 0f;
            }
		}
		private void Swing()
		{
			if (this.body && this.body.master)
            {
				Ray aimRay = this.GetAimRay();
				this.reloaded = false;
			}
		}
		private Ray GetAimRay()
		{
			if (this.inputBank)
			{
				return new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
			}
			return new Ray(base.transform.position, base.transform.forward);
		}

		public float totalReloadTime = 0f;
		public float damageCoefficientBase = 0f;
		public float damageCoefficientPerStack = 0f;
		public float force = 0f;
		private SkillLocator skillLocator;
		private float reloadTimer;
        private bool reloaded = false;
		private InputBankTest inputBank;
	}
}
