-- Milk
local effects = require('Scripts/effects')
function startSpell()
end

function completeSpell()
end

function useItem()
    StatusEffect(effects.MINOR_HEALING, "Minor Healing", 3575790706, 3, 30, 0)
end
