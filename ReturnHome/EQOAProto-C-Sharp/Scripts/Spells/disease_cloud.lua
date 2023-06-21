-- Disease Cloud
local effects = require("Scripts/effects")
local spellFX = 0x052EF90F  --Spell Effect
local buffIcon = 0xCF3C577B
local recast = 1 --Recast Time
local castTime = 1
local buffName = "Disease Cloud"
local effectID = effects.DISEASE_CLOUD
local duration = 24
local damage = 4
local powerCost = 8
local tier = 0

function startSpell()
    CastSpell(session, spellFX, entity.ObjectID, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    AddStatusEffect(effectID, buffName, buffIcon, duration, tier, entity.ObjectID)
end

function tickSpell(entity)
    print("Ticking")
    Damage(session, damage, Effect.casterID, entity.ObjectID)
    entity.CurrentHP = (entity.CurrentHP-damage)
    print(entity.CharName)
end

function useItem()
end
