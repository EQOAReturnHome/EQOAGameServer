-- Milk
local effects = require('Scripts/effects')
function startSpell()
end

function completeSpell()
end

function useItem()
    print("Minor Healing") 
    StatusEffect("Minor Healing", 3575790706)
end
