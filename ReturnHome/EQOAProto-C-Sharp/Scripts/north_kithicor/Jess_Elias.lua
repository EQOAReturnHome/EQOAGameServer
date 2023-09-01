function event_say()
diagOptions = {}
    npcDialogue = "Harold is so stubborn. He is blinded by his distaste of the halflings. We need to ally with them if we have any hope for our village to see another summer."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end