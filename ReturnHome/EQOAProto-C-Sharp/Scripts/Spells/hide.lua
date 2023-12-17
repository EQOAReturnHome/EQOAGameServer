-- Hide
local spellFX = 0x1A2B8058  --Spell Effect
local recast = 6 --Recast Time
local castTime = 0
local duration = 300
local effects = require("Scripts/effects")
local buffIcon = 0x9CD122E0 
local buffName = "Hide"
local effectID = effects.HIDE
local tier = 0
local effectType = 2

function startSpell()
    CastSpell(session, spellFX, entity.ObjectID, target, castTime)
    CoolDown(session, addedOrder, recast)
end

function completeSpell()
    onEffect()
end

function onEffect()
AddStatusEffect(effectID, buffName, buffIcon, duration, tier, entity.ObjectID, effectType)
UpdateSpeed(entity, 2)
UpdateInvis(entity, 1)
end

function onEffectEnd()
UpdateSpeed(entity, entity.baseSpeed)
UpdateInvis(entity, 0)
end

