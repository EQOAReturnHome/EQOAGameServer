function  event_say(choice)
diagOptions = {}
    npcDialogue = "Oi!!  You 'aven't got a few tunar to spare so an old man can get a wee' bit o' mead in 'is belly, do ye?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end