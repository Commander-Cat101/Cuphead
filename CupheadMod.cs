using MelonLoader;
using BTD_Mod_Helper;
using CupheadMod;
using BTD_Mod_Helper.Api.Towers;
using System.Linq;
using BTD_Mod_Helper.Api.Enums;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Extensions;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api;
using Il2CppSystem;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities;
using Assets.Scripts.Models.Towers.Behaviors;
using mugman;
using Assets.Scripts.Simulation.Towers.Emissions.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Emissions.Behaviors;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using Assets.Scripts.Models.GenericBehaviors;
using Assets.Scripts.Simulation.Towers.Weapons;
using UnityEngine.UI;
using System;
using Il2CppSystem.Threading.Tasks;
using static MelonLoader.MelonLogger;
using String = Il2CppSystem.String;

[assembly: MelonInfo(typeof(Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
public class Main : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {

        ModHelper.Msg<Main>("Cuphead loaded!");
    }
    
    
}
namespace CupheadMod
{
    public class PeaShooterDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "PeaShooterDisplay");
        }
    }
    public class MiniPeaShooterDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "MiniPeaShooterDisplay");
        }
    }
    public class SpreadDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "SpreadDisplay");
        }
    }
    public class ChaserDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "ChaserDisplay");
        }
    }
    public class MiniBombDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "MiniBombDisplay");
        }
    }
    public class MugmanDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "MugmanDisplay");
        }
    }

}
namespace Cuphead
{
    public class Cuphead : ModTower
    {

        public override string TowerSet => TowerSetType.Magic;
        public override string BaseTower => TowerType.DartMonkey;
        public override int Cost => 500;
        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 0;

        public override bool Use2DModel => true;
        public override string Portrait => "Cuphead-Icon";

        public override string Icon => "Cuphead-Icon";
        public override bool DontAddToShop => false;
        public override string Description => "Cuphead is here";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.GetAttackModel().weapons[0].projectile.ApplyDisplay<PeaShooterDisplay>();
        }
        public override string Get2DTexture(int[] tiers)
        {
            return "CupheadDisplay";
        }
    }
    public class BetterPeashooter : ModUpgrade<Cuphead>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Cuphead-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 650;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "A better peashooter deals more damage";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile.GetDamageModel().damage += 1;
        }

    }
    public class Spreadshot : ModUpgrade<Cuphead>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Cuphead-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 850;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Shoots 3 bullets at a time";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile.ApplyDisplay<SpreadDisplay>();
            attackModel.weapons[0].emission = new ArcEmissionModel("ArcEmissionModel_", 3, 0, 90, null, false);
        }

    }
    public class LongerRange : ModUpgrade<Cuphead>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Cuphead-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 900;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Bullets travel further";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            attackModel.range += 15f;
            towerModel.range += 15f;
        }
    }
    public class Chaser : ModUpgrade<Cuphead>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Cuphead-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 2400;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Bullets seek out near by bloons";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile.ApplyDisplay<ChaserDisplay>();
            projectile.AddBehavior(new TrackTargetModel("Testname", 9999999, true, false, 144, false, 300, false, true));
            projectile.GetDamageModel().damage += 1;
        }
    }
    public class SuperArtI : ModUpgrade<Cuphead>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Cuphead-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 55000;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Super Art I allows Cuphead to shoot at high speeds";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            var Ability = Game.instance.model.GetTowerFromId("BoomerangMonkey-040").GetBehavior<AbilityModel>().Duplicate();
            Ability.GetBehavior<TurboModel>().projectileDisplay.assetPath = ModContent.CreatePrefabReference<ChaserDisplay>();
            Ability.GetBehavior<TurboModel>().multiplier = .05f;
            towerModel.AddBehavior(Ability);
            towerModel.GetBehavior<AbilityModel>().icon = GetSpriteReference(mod, "SuperArtI-Icon");
            projectile.GetDamageModel().damage *= 2;
            attackModel.weapons[0].Rate *= .2f;
        }
    }
}
namespace mugman
{
    public class Mugman : ModTower
    {
        public override string BaseTower => "HeliPilot-210";
        public override int Cost => 650;
        public override string Description => "Mugman takes to the the skys to shoot down the bloons";
        public override string Portrait => "Mugman-Icon";
        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 0;

        public override string TowerSet => TowerSetType.Magic;

        // 2D shit
        public override bool Use2DModel => true;
        public override float PixelsPerUnit => 5f;
        public override string Get2DTexture(int[] tiers)
        {

            return "HeliPadDisplay";
        }

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.GetBehavior<AirUnitModel>().display = ModContent.CreatePrefabReference<MugmanDisplay>();

            var heliMovementModel = towerModel.GetBehavior<AirUnitModel>().behaviors.First().Cast<HeliMovementModel>();
            heliMovementModel.maxSpeed = 80;
            heliMovementModel.rotationSpeed = 0.16f;
            heliMovementModel.movementForceStart = 600;
            heliMovementModel.movementForceEnd = 300;
            heliMovementModel.movementForceEndSquared = 90000;
            heliMovementModel.brakeForce = 400;
            heliMovementModel.otherHeliRepulsonForce = 15;

            towerModel.range = 22f;
            towerModel.radius = 6;
            towerModel.radiusSquared = 36.0f;

            towerModel.GetBehavior<RectangleFootprintModel>().xWidth = 5;
            towerModel.GetBehavior<RectangleFootprintModel>().yWidth = 5;

            var footPrint = towerModel.GetBehavior<RectangleFootprintModel>();
            towerModel.footprint = footPrint;

            var attackModel = towerModel.GetAttackModel();

            attackModel.RemoveWeapon(attackModel.weapons[1]); // remove quad darts
            attackModel.weapons[0].emission = Game.instance.model.GetTowerFromId("HeliPilot-400").GetAttackModel().weapons[2].emission;
            attackModel.weapons[0].projectile.pierce = 1;
        }
    }
}
public class MiniPeashots : ModUpgrade<Mugman>
{
    public override string Portrait => "Mugman-Icon.png";
    public override int Path => MIDDLE;
    public override int Tier => 1;
    public override int Cost => 650;
    public override string Description => "Bullets split into 2 mini peashots";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;

        var splitmodel = Game.instance.model.GetTowerFromId("MonkeySub-002").GetWeapon().projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().Duplicate();
        splitmodel.projectile.ApplyDisplay<MiniPeaShooterDisplay>();
        splitmodel.fraction = .9f;           
        splitmodel.projectile.GetDamageModel().damage = 1;
        splitmodel.projectile.pierce = 1;
        splitmodel.emission = new ArcEmissionModel("ArcEmissionModel_", 2, 0, 0, null, true);
        splitmodel.emission.AddBehavior(new EmissionRotationOffProjectileDirectionModel("rotate", -20, 20, false));
        projectile.AddBehavior(splitmodel);
    }
}
public class TriplePeashots : ModUpgrade<Mugman>
{
    public override string Portrait => "Mugman-Icon.png";
    public override int Path => MIDDLE;
    public override int Tier => 2;
    public override int Cost => 800;
    public override string Description => "Fires 3 mini peashots instead of 2";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;

        var splitmodel = projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>();
        splitmodel.projectile.ApplyDisplay<MiniPeaShooterDisplay>();
        splitmodel.projectile.GetDamageModel().damage = 1;
        splitmodel.projectile.pierce = 2;
        splitmodel.emission = new ArcEmissionModel("ArcEmissionModel_", 3, 0, 0, null, false);
        splitmodel.emission.AddBehavior(new EmissionRotationOffProjectileDirectionModel("rotate", -20, 20, false));
        projectile.AddBehavior(splitmodel);
    }
}
public class HomingBullets : ModUpgrade<Mugman>
{
    public override string Portrait => "Mugman-Icon.png";
    public override int Path => MIDDLE;
    public override int Tier => 3;
    public override int Cost => 1200;
    public override string Description => "Bullets seek out nearby bloons";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        projectile.AddBehavior(new TrackTargetModel("Testname", 9999999, true, false, 144, false, 300, false, true));
        attackModel.range *= 1.7f;
    }
}
public class MiniBombs : ModUpgrade<Mugman>
{
    public override string Portrait => "Mugman-Icon.png";
    public override int Path => MIDDLE;
    public override int Tier => 4;
    public override int Cost => 8700;
    public override string Description => "Shoots explosive mini bombs instead of bullets";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        attackModel.weapons[0].Rate *= .6f;
        var projectile = attackModel.weapons[0].projectile;
        projectile.GetDamageModel().damage *= 2f;
        projectile.pierce += 1f;
        projectile.ApplyDisplay<MiniBombDisplay>();
        projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-400").GetAttackModel().weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());
        projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-400").GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate());
    }
}
public class BigBombs : ModUpgrade<Mugman>
{
    public override string Portrait => "Mugman-Icon.png";
    public override int Path => MIDDLE;
    public override int Tier => 5;
    public override int Cost => 45500;
    public override string Description => "Mini Bombs become MEGA bombs";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var projectile = attackModel.weapons[0].projectile;
        attackModel.weapons[0].Rate *= .3f;
        projectile.GetDamageModel().damage *= 11f;
        projectile.pierce += 1f;
        var proj = projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
        proj.GetDamageModel().damage *= 6;
        projectile.RemoveBehavior<CreateEffectOnContactModel>();
        projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-500").GetAttackModel().weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());
    }
}
