function  event_say(choice)
diagOptions = {}
    npcDialogue = "I invited Anya here after she lost her family. She is a hard worker, and a good friend."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end