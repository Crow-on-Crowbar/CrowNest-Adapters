#include "TemplateAdapter.h"

void TemplateAdapter::Attach()
{
	_logger.Info("attached.");
}

void TemplateAdapter::Detach()
{
	_logger.Info("detached.");
}

std::string TemplateAdapter::GetGameName()
{
	return "Unknown";
}
