--Minor Healing
local effects = require("Scripts/effects")
local spellFX = 0xC2B700FA  --Spell Effect
local buffIcon = 0x828563AF
local recast = 1 --Recast Time
local buffName = "Minor Healing"
local effectID = effects.MINOR_HEALING
local castTime = 2
local duration = 18
local heal = 16 + (entity.Charisma*.25)
local tier = 0

function startSpell()
   CastSpell(session, spellFX, entity.ObjectID, target, castTime)
   CoolDown(session, addedOrder, recast)
end

function completeSpell()
   if(entity.CurrentHP <= entity.HPMax)then
      entity.CurrentHP = (heal+entity.CurrentHP)
   end
   AddStatusEffect(effectID, buffName, buffIcon, duration, tier, entity.ObjectID)
end

function tickSpell(entity)
   if(entity.CurrentHP <= entity.HPMax)then
      entity.CurrentHP = (heal+entity.CurrentHP)
   end
end

function useItem()
end

