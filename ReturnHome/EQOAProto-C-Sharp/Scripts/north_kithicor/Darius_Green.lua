function event_say()
diagOptions = {}
    npcDialogue = "I wouldn't go up there if I was you. Proudfeather is as protective of the nests as they come. Best to be on your way."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end