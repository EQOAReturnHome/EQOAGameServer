-- Kick
function spell()
    spellFX = 0x0A286E2E --Spell Effect
    damage = -115 --Spell Damage
    recast = 0 --Recast Time
    CastSpell(session, spellFX, target, 3)
    print("Kicking")

    Damage(session, damage, target)
    CoolDown(session, addedOrder, recast)
end
