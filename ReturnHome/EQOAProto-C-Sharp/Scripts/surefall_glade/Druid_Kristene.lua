function  event_say(choice)
diagOptions = {}
    npcDialogue = "You'll find the bears and wolves in the glade to be quite peaceful. They are here to help protect the trees and all life."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end