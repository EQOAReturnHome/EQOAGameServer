-- Milk
local effects = require('Scripts/effects')
function startSpell()
end

function completeSpell()
end

function useItem()
    StatusEffect(effects.MINOR_HEALING, "Minor Healing", 2189779887, 3, 30, 0)
    --StatusEffect(2, "Weak Hands", 667007739, 3, 30, 0)
end
