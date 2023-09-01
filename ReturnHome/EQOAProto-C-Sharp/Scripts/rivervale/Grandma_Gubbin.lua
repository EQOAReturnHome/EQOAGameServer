function  event_say(choice)
diagOptions = {}
    npcDialogue = "Only Donpo calls me Old Lady. He has called me that since we were in school together. I believe he has secretly fancied me for all these years."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end