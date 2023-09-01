function event_say()
diagOptions = {}
    npcDialogue = "We cannot do this any more, Aneric. The world calls to me. I cannot stay here any longer. You must let me go."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end