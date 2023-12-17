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
local effectType = 1

function startSpell()
    CastSpell(session, spellFX, entity.ObjectID, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    onEffect()
end

function tickSpell(entity)
    Damage(session, damage, Effect.casterID, entity.ObjectID)
    entity.CurrentHP = (entity.CurrentHP-damage)
end

function onEffect()
    AddStatusEffect(effectID, buffName, buffIcon, duration, tier, entity.ObjectID, effectType)
 end
 
 function onEffectEnd(entity)
 
 end

function useItem()
end
