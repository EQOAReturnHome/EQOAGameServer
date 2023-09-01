function event_say()
diagOptions = {}
    npcDialogue = "I can't shake these awful visions of fanged teeth, sharp claws, and death. Are the druidic ruins trying to warn me?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end