function  event_say(choice)
diagOptions = {}
    npcDialogue = "Any enemy who dares invade Qeynos shall be met with my blade."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end