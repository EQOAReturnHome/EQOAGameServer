function event_say()
diagOptions = {}
    npcDialogue = "To express their unwavering devotion, paladins of Paragon Keep are equipped with the Sword of Faith. This sword is a conduit through which a paladin will deliver the will and protection of the goddess."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end