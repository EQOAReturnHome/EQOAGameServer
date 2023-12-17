--Minor Healing
local effects = require("Scripts/effects")
local spellFX = 0xC2B700FA  --Spell Effect
local buffIcon = 0xDA6F7DE9
local recast = 1 --Recast Time
local buffName = "Minor Blessing"
local effectID = effects.MINOR_BLESSING
local castTime = 3
local duration = 18
local tier = 0


function startSpell()
   CastSpell(session, spellFX, entity.ObjectID, target, castTime)
   CoolDown(session, addedOrder, recast)
end

function completeSpell()
   AddStatusEffect(effectID, buffName, buffIcon, duration, tier, entity.ObjectID)
end

function onEffectEnd(entity)
   print("MINOR BLESSING")
end

function useItem()
   LearnSpell(session, GetSpell(977))
end

