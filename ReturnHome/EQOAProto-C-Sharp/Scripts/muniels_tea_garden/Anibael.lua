function event_say()
diagOptions = {}
    npcDialogue = "Armis knows more than he is letting on, watch your back if you ever have any dealings with him."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end