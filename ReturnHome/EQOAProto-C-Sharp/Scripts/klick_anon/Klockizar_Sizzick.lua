function  event_say(choice)
diagOptions = {}
    npcDialogue = "They said I spread a virus all over the city, but it wasn't me, it was some other necromancer...yeah, that's right, some other guy."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end