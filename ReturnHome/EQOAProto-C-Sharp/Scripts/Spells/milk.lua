-- Milk
local effects = require('Scripts/effects')
function startSpell()
end

function completeSpell()
end

function useItem()
    StatusEffect(effects.MINOR_HEALING, "Minor Healing", 2189779887, 30, 0)
end
