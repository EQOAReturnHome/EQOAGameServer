function  event_say(choice)
diagOptions = {}
    npcDialogue = "Cured drakes blood� Can't say I've used that before but�Oh it shouldn't be a problem mixing  with embalming fluid�I think�Hmm."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end